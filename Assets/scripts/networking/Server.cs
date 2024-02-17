using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Game_package;
using System.Collections.Generic;


public class Server : MonoBehaviour
{
    private const int PORT = 8888;
    private TcpListener listener;
    private bool isRunning = false;
    List<TcpClient> clients = new List<TcpClient>();
    private GameController gameController = new GameController();

    void Start()
    {
        StartServer();
    }

    void StartServer()
    {
        try
        {
            if (!isRunning)
            {
                listener = new TcpListener(IPAddress.Any, PORT);
                listener.Start();
                Debug.Log("Server is running on port " + PORT);
                isRunning = true;

                // Start accepting client connections in a separate thread
                Thread thread = new Thread(ListenForClients);
                thread.Start();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to start server: " + e.Message);
        }
    }

    void ListenForClients()
    {
        while (isRunning)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                Debug.Log("Client connected from " + client.Client.RemoteEndPoint);
                clients.Add(client);
                // Handle client communication in a separate thread
                Thread clientThread = new Thread(() => HandleClientCommunication(client));
                clientThread.Start();
            }
            catch (Exception e)
            {
                // Debug.LogError("Error accepting client connection: " + e.Message);
            }
        }
    }

    void HandleClientCommunication(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            // Read the message type
            int bytesRead = stream.Read(buffer, 0, sizeof(int));
            if (bytesRead < sizeof(int))
            {
                Debug.LogError("Failed to read message type.");
                return;
            }

            // Convert received message type to enum
            MessageType messageType = (MessageType)BitConverter.ToInt32(buffer, 0);

            // Process message based on type
            switch (messageType)
            {
                case MessageType.PlayerID:
                    // Read player ID
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    int playerId = BitConverter.ToInt32(buffer, 0);
                    Debug.Log(playerId);
                    if (playerId > 0)
                    {
                        Debug.Log("Received player ID from client: " + playerId);
                        Game game = gameController.askForGame(playerId);
                        if (game != null)
                        {
                            string jsonGameData = JsonUtility.ToJson(game);

                            // Serialize the game data directly
                            byte[] jsonBytes = System.Text.Encoding.ASCII.GetBytes(jsonGameData);

                            // Send game data to client
                            foreach (TcpClient c in clients)
                            {
                                NetworkStream stream1 = c.GetStream();
                                stream.Write(jsonBytes, 0, jsonBytes.Length);
                                Debug.Log("Sent game data to client.");
                            }

                        }
                    }
                    else
                    {
                         Debug.LogError("Failed to parse player ID: " );
                    }
                    break;
                case MessageType.GameData:
                    // Read game data
                    break;
                // Handle other message types as needed
                default:
                    Debug.LogError("Unknown message type received.");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error handling client communication: " + e.Message);
        }
       
    }


    void OnDestroy()
    {
        StopServer();
    }

    void StopServer()
    {
        isRunning = false;
        if (listener != null)
        {
            listener.Stop();
        }
    }
}
