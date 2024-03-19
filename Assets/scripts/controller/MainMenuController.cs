using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Client client;
    private GameModel gameModel = GameModel.getInstance();


    void Start()
    {
        // Assuming you have a Client component attached to the same GameObject
        client = Client.getInstance();
    }
   

    public void onStartGameClick()
    {

        Debug.Log("startGameClicke --");
        if(!client.connect){
            OnLogOutClick();
            return;
        }
        // Start loading the waiting room scene
        SceneManager.LoadScene("waitingRoom");
        Debug.Log("sceneLoaded --");
        // Request a game from the server
        client.RequestGame(gameModel);
    }

 

    public void OnLogOutClick()
    {
        client.Close();
        Debug.Log("logout");
        SceneManager.LoadScene("Login");
    }
    void OnDestroy()
    {
        // Unsubscribe from the scene changing event
       
    }
}
