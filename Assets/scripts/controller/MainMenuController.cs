using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Client client;

    void Start()
    {
        // Assuming you have a Client component attached to the same GameObject
        client = Client.getInstance();
    }

    public void onStartGameClick()
    {

        SceneManager.LoadScene("waitingRoom");
        client.RequestGame();
        client.GetGame();
        SceneManager.LoadScene("Game");


    }

}
