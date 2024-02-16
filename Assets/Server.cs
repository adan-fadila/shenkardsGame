using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Game_package;
using System.Collections.Generic;
using Player_package;

public class TCPServer : MonoBehaviour
{
    public int port = 8888; // Port number for the server
    private TcpListener listener;
    private bool isRunning = false;
     private List<int> connectedPlayers = new List<int>();
     private List<Game> activeGames = new List<Game>();
    private GameService gameService = new GameService();

    void Awake()
    {
        StartServer();
    }

    private void StartServer()
    {
        try
        {
            if (!isRunning)
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Debug.Log("Server is running on port " + port);
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

    private void ListenForClients()
    {
        while (isRunning)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                Debug.Log("Client connected from " + client.Client.RemoteEndPoint);

                // Handle client communication in a separate thread
                Thread clientThread = new Thread(HandleClientCommunication);
                clientThread.Start(client);
            }
            catch (Exception e)
            {
                Debug.LogError("Error accepting client connection: " + e.Message);
            }
        }
    }

    private void HandleClientCommunication(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Debug.Log("Received from client " + client.Client.RemoteEndPoint + ": " + dataReceived);

            // Echo the received data back to the client
            byte[] responseData = Encoding.ASCII.GetBytes("Server echo: " + dataReceived);
            stream.Write(responseData, 0, responseData.Length);
        }

        client.Close();
    }
     public void HandlePlayerJoin(int playerId)
    {
        connectedPlayers.Add(playerId);
        if (gameService != null && gameService.IsWaitingPlayerAvailable())
        {
            // If there's a waiting player, start the game
           
        }
        else
        {
            // Player is waiting for another player to join
            gameService.SetWaitingPlayer(playerId);
        }
    }

    // private void Startgame(Player waitingPlayer)
    // {
    //     // Logic to start the ga with waiting player
    // }
    

    void OnDestroy()
    {
        StopServer();
    }

    private void StopServer()
    {
        isRunning = false;
        if (listener != null)
        {
            listener.Stop();
        }
    }
}
