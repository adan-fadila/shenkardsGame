using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using SharedLibrary;

public class Client
{
    private static Client instance;
    int playerId = -1;
    private TcpClient client;
    private NetworkStream stream;
    private const string serverAddress = "127.0.0.1"; // Server IP address
    private const int port = 8888;

    private Client()
    {
        client = new TcpClient(serverAddress, port);
        stream = client.GetStream();
    }
    public static Client getInstance()
    {
        if (instance == null)
        {
            instance = new Client();
        }
        return instance;
    }
    public void Login(string username, string password)
    {
        // Example: Sending login request to the server
        string loginRequest = $"LOGIN|{username}|{password}";
        SendMessage(loginRequest);
        ReceivePlayerId();
    }

    public void RequestGame()
    {
        // Example: Sending game request with player ID
        string gameRequest = $"GAME_REQUEST|{playerId}";
        SendMessage(gameRequest);

    }
    public GameData GetGame()
    {
        try
        {
            byte[] buffer = new byte[1024];
            while (true)
            {

                if (stream.DataAvailable)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string serializedGameData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    GameData gameData = JsonConvert.DeserializeObject<GameData>(serializedGameData);
                    return gameData;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("GetGame exseption: " + e);
        }
        return null;
    }


    private void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    public void ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
    }
    public void ReceivePlayerId()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string playerIdString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        int playerId;
        if (int.TryParse(playerIdString, out playerId))
        {
            this.playerId = playerId;
        }
        Debug.Log(this.playerId);

    }

    public void Close()
    {
        stream.Close();
        client.Close();
    }

}



