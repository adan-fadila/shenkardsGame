using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class client : MonoBehaviour
{
    private const int PORT = 8888;
    private const string SERVER_IP = "127.0.0.1"; // Change this to your server's IP address

    private Socket clientSocket;
    [Serializable]
    public class gameState{
        public int playerScore;
        public string playerName;

    }

    void Start()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            clientSocket.BeginConnect(SERVER_IP, PORT, ConnectCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error connecting to server: " + e.Message);
        }
    }

    private void ConnectCallback(IAsyncResult result)
    {
        if (clientSocket.Connected)
        {
            Debug.Log("Connected to server");
            ReceiveData();
        }
        else
        {
            Debug.Log("Failed to connect to server");
        }
    }

    void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, buffer);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int bytesRead = clientSocket.EndReceive(result);
            if (bytesRead > 0)
            {
                byte[] data = result.AsyncState as byte[];
                string message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                Debug.Log("Received message from server: " + message);
            }
            ReceiveData(); // Continue receiving data
        }
        catch (Exception e)
        {
            Debug.Log("Error receiving data: " + e.Message);
        }
    }

    void SendData(string message)
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, null);
    }

    private void SendCallback(IAsyncResult result)
    {
        try
        {
            clientSocket.EndSend(result);
            Debug.Log("Message sent to server");
        }
        catch (Exception e)
        {
            Debug.Log("Error sending data: " + e.Message);
        }
    }

    void OnDestroy()
    {
        if (clientSocket != null && clientSocket.Connected)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
