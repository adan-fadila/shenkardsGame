using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using SharedLibrary;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public class Client
{
    private static Client instance = null;
    public int playerId = -2;
    private TcpClient client;
    public bool connect = false;
    private NetworkStream stream;
    public bool signup = false;
    private const string serverAddress = "54.196.107.218"; // Server IP address
    private const int port = 8888;

    private Client()
    {
        client = new TcpClient(serverAddress, port);
        stream = client.GetStream();
        connect = true;
        Task.Run(() => receiveMessage());
    }
    public static Client getInstance()
    {
        if (instance == null)
        {
            instance = new Client();
        }
        return instance;
    }

    public void receiveMessage()
    {
        while (true)
        {
            byte[] buffer = new byte[6000];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            string[] parts = message.Split('|');
            if (parts[0] == "LOGIN")
            {
                Debug.Log("login");
                ReceivePlayerId(parts[1]);
            }
            if (parts[0] == "SignUp")
            {
                Debug.Log("login");
                signup = ReceiveSignUpMsg(parts[1]);
            }
            if (parts[0] == "GameData")
            {
                ReceiveGameData(GameModel.getInstance(), parts[1]);
            }
            if (parts[0] == "GameEnd")
            {
                GameModel.getInstance().winners = JsonConvert.DeserializeObject<List<int>>(parts[1]);
            }
            if (parts[0] == "PlayerExit")
            {
                Debug.Log("playerExit");
                GameModel.getInstance().gameEnd = true;

            }
            if (parts[0] == "ForceLogOut")
            {
                Debug.Log("ForceLogOut");
                connect = false;

            }
        }

    }
    public void Login(string username, string password)
    {
        // Example: Sending login request to the server
        string loginRequest = $"LOGIN|{username}|{password}";
        SendMessage(loginRequest);

    }

 
    public void SignUp(string username, string password)
    {
        string signUprequest = $"SignUp|{username}|{password}";
        SendMessage(signUprequest);

    }



    public void RequestGame(GameModel gameModel)
    {
        // Example: Sending game request with player ID
        string gameRequest = $"GAME_REQUEST|{playerId}";
        SendMessage(gameRequest);

    }
    private void ReceiveGameData(GameModel gameModel, string Data)
    {
        GameData gameData = GetGame( Data);

        gameModel.gameData = gameData;
        Debug.Log("dataUpdate");
 
    }



    public GameData GetGame(string Data)
    {
        GameData gameData = null;
        try
        {

            Debug.Log(Data);
            gameData = JsonConvert.DeserializeObject<GameData>(Data);
            Debug.Log(gameData.player2.PlayeName);



        }
        catch (Exception e)
        {
            Console.WriteLine("GetGame exseption: " + e);
        }

        return gameData;
    }



    public void EndTurn(List<PlayedCard> playedCards, GameModel gameModel)
    {
        string cards = JsonConvert.SerializeObject(playedCards);
        string EndTurn = $"EndTurn|{cards}";
        SendMessage(EndTurn);

    }


    public void ExitGame()
    {
        SendMessage("ExitGame");
    }

    private void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    public bool ReceiveSignUpMsg(string receivedMessage)
    {

        if (receivedMessage == "success")
        {
            return true;
        }
        return false;

    }

    public void ReceivePlayerId(string playerIdString)
    {

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
        instance = null;
    }

}



