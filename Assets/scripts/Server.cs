using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Threading;

public class Server : MonoBehaviour
{
    private const int PORT = 8888;
    private TcpListener listener;
    private bool isRunning = false;

    void Start()
    {
        StartServer();
    }

    void StartServer()
    {
        try
        {
            listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();
            Debug.Log("Server is running on port " + PORT);
            isRunning = true;

            // Start accepting client connections in a separate thread
            Thread thread = new Thread(ListenForClients);
            thread.Start();
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

            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string playerIdString = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Convert received player ID string to integer
            int playerId;
            if (int.TryParse(playerIdString, out playerId))
            {
                Debug.Log("Received player ID from client: " + playerId);
                // Do whatever you need to do with the player ID here
            }
            else
            {
                Debug.LogError("Failed to parse player ID: " + playerIdString);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error handling client communication: " + e.Message);
        }
        finally
        {
            client.Close();
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
