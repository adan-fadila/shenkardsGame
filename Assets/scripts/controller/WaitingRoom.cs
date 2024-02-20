
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoom : MonoBehaviour
{
    private  GameModel gameModel;
    private Client client;
    // Start is called before the first frame update
    void Start()
    {
        client = Client.getInstance();
        gameModel = GameModel.getInstance();
        client.RequestGame();
        
    }

    void Update()
    {
        try
        {
            gameModel.gameData = client.GetGame();
            SceneManager.LoadScene("Game");
        }
        catch (System.Exception)
        {

           Debug.Log("no game data");
        }
    }
}
