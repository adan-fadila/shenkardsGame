
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoom : MonoBehaviour
{
    private GameModel gameModel;
    private Client client;
    // Start is called before the first frame update
    void Start()
    {
        client = Client.getInstance();
        gameModel = GameModel.getInstance();
        StartCoroutine(WaitForGameData());
    }

    IEnumerator WaitForGameData()
    {
        // Wait until the game data is received from the server
        while (gameModel.gameData == null && client.connect)
        { 
            yield return null;
        }
        Debug.Log("not while");
        if (!client.connect)
        {
            client.Close();
            SceneManager.LoadScene("Login");
        }
        else { SceneManager.LoadScene("Game"); }
       

    }
}
