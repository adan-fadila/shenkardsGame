using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Game_package;

public class Client : MonoBehaviour
{
    private const string SERVER_IP = "127.0.0.1"; // Change this to your server's IP address
    private const int PORT = 8888;
 private GameUI gameUI = GameUI.getInstance();


    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            TcpClient client = new TcpClient(SERVER_IP, PORT);
            Debug.Log("Connected to server.");

            // Send request for game data
            SendMessage(client, MessageType.PlayerID, PlayerName.getPlayerId());

            // Receive game data
            ReceiveMessage(client);
            // if (!string.IsNullOrEmpty(jsonGame))
            // {
            //     // Deserialize game data
            //     Game game = JsonUtility.FromJson<Game>(jsonGame);
            //     Debug.Log("Received game data: " + game.locations[0].Name + ", " + game.locations[0].Desc);
            // }

            // client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    void SendMessage(TcpClient client, MessageType messageType, int playerId)
    {
        try
        {
            NetworkStream stream = client.GetStream();

            // Convert message type to bytes
            byte[] messageTypeBytes = BitConverter.GetBytes((int)messageType);

            // Send message type to server
            stream.Write(messageTypeBytes, 0, messageTypeBytes.Length);
            Debug.Log("Sent message type to server: " + messageType);

            // If player ID is provided, send it to the server
            if (messageType == MessageType.PlayerID)
            {
                byte[] playerIdBytes = BitConverter.GetBytes(playerId);
                stream.Write(playerIdBytes, 0, playerIdBytes.Length);
                Debug.Log("Sent player ID to server: " + playerId);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending message to server: " + e.Message);
        }
    }
    void ReceiveMessage(TcpClient client)
    {
       
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            // Read JSON data
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string jsonGameData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Deserialize game data
            Game game = JsonUtility.FromJson<Game>(jsonGameData);
            gameUI.UpdateGame(game);
            // Debug.Log("Received game data: " + game.Score + ", " + game.PlayerName);
            // Update UI or game state with received game data
            // UpdateGameState(game);
        
       
    }
}