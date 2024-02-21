
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
        Debug.Log("seceneStarted");
        client = Client.getInstance();
        gameModel = GameModel.getInstance();
        StartCoroutine(WaitForGameData());
    }

    IEnumerator WaitForGameData()
    {
        // Wait until the game data is received from the server
        while (gameModel.gameData == null)
        {
            yield return null;
        }

        // Once game data is received, proceed to the game scene
        SceneManager.LoadScene("Game");
    }
}
