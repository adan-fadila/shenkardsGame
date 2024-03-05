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
    public int playerId = -1;
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
    public bool Login(string username, string password)
    {
        // Example: Sending login request to the server
        string loginRequest = $"LOGIN|{username}|{password}";
        SendMessage(loginRequest);
        ReceivePlayerId();
        if (playerId == -1)
        {
            return false;
        }
        return true;
    }

    public bool AdminLogin(string username, string password)
    {
        // Example: Sending login request to the server
        string loginRequest = $"AdminLOGIN|{username}|{password}";
        SendMessage(loginRequest);
        ReceivePlayerId();
        if (playerId == -1)
        {
            return false;
        }
        return true;
    }
    public bool SignUp(string username, string password){
        string signUprequest = $"SignUp|{username}|{password}";
        SendMessage(signUprequest);
        return ReceiveSignUpMsg();
    }













    public void RequestGame(GameModel gameModel)
    {
        // Example: Sending game request with player ID
        string gameRequest = $"GAME_REQUEST|{playerId}";
        SendMessage(gameRequest);
        Task.Run(() => ReceiveGameData(gameModel));
    }
    private void ReceiveGameData(GameModel gameModel)
    {
        // Receive game data from the server
        GameData gameData = GetGame(gameModel);

        // Update the game model with the received game data
        gameModel.gameData = gameData;
        // If game data is received, load the game scene
    }



    public GameData GetGame(GameModel gameModel)
    {
        GameData gameData = null;
        while (gameData == null && gameModel.winners.Count == 0 && !gameModel.gameEnd)
        {
            try
            {
                byte[] buffer = new byte[5024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string serializedGameData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string[] parts = serializedGameData.Split('|');
                if(parts[0] == "PlayerExit"){
                    Debug.Log("playerExit");
                    gameModel.gameEnd = true;
                    return null;
                }
                if(parts[0] == "gameEnd"){
                    Debug.Log("gameEnd");
                    gameModel.winners = JsonConvert.DeserializeObject<List<int>>(parts[1]);
                    return null;
                }
                Debug.Log(serializedGameData);
                gameData = JsonConvert.DeserializeObject<GameData>(serializedGameData);
                Debug.Log(gameData.player2.PlayeName);



            }
            catch (Exception e)
            {
                Console.WriteLine("GetGame exseption: " + e);
            }
        }
        return gameData;
    }












    

    public void EndTurn(List<PlayedCard> playedCards, GameModel gameModel)
    {
        string cards = JsonConvert.SerializeObject(playedCards);
        string EndTurn = $"EndTurn|{cards}";
        SendMessage(EndTurn);
        Task.Run(() => ReceiveGameData(gameModel));
    }


    public void ExitGame(){
        SendMessage("ExitGame");
    }

    private void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    public bool ReceiveSignUpMsg()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        if(receivedMessage == "success"){
            return true;
        }
        return false;

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



