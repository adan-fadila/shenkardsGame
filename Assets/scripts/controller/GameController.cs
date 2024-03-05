using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedLibrary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public Text Name;
    public Text Energy;
    public Button EndTurn;
    public Text OppName;
    private GameModel gameModel;
    private Client client;
    public static PlayerData playerData;
    public GameObject cardsManagerObj;
    public GameObject locationsManagerObj;
    void Awake()
    {

        gameModel = GameModel.getInstance();
        client = Client.getInstance();
        playerData = new PlayerData();
        setGameData();

    }
    private void setGameData()
    {
        if (gameModel.gameData.player1.PlayeId == client.playerId)
        {
            playerData = gameModel.gameData.player1;
            this.OppName.text = gameModel.gameData.player2.PlayeName;
        }
        else
        {
            playerData = gameModel.gameData.player2;
            this.OppName.text = gameModel.gameData.player1.PlayeName;
        }
        this.Name.text = $"{playerData.PlayeName}";
        Energy.text = $"{playerData.Energy}";

    }
    void Update()
    {
        if (gameModel.gameData != null)
            Energy.text = $"{playerData.Energy}";

    }
    public void onEndButtonClick()
    {
        client.EndTurn(PlayedCardsModel.playedCards, gameModel);
        gameModel.gameData = null;
        EndTurn.interactable = false;
        StartCoroutine(WaitForGameData());
    }
    IEnumerator WaitForGameData()
    {
        CardsManager cardsManager = cardsManagerObj.GetComponent<CardsManager>();
        LocationManager locationManager = locationsManagerObj.GetComponent<LocationManager>();
        // Wait until the game data is received from the server
        while (gameModel.gameData == null && !gameModel.gameEnd && gameModel.winners.Count == 0)
        {
            yield return null;
        }

        if (gameModel.gameEnd)
        {
            onExitClick();
            client.ExitGame();
            Debug.Log("gameEnd");
        }
        else
        {
            if (gameModel.winners.Count > 0)
            {
                Debug.Log("gameWin");
            }
            else
            {
                setGameData();

                if (cardsManager != null)
                {
                    destroyCards();
                    cardsManager.CreateInstances();
                }
                if (locationManager != null)
                {
                    destroyLocations();
                    locationManager.CreateInstances();
                }
                EndTurn.interactable = true;
                PlayedCardsModel.playedCards = new List<PlayedCard>();
            }
        }

    }
    public void onExitClick()
    {
        client.ExitGame();
        gameModel.gameData = null;
        gameModel.gameEnd = false;
        gameModel.winners = new List<int>();
        SceneManager.LoadScene("MainMenu");
    }
    void destroyLocations(){
       foreach (Transform item in locationsManagerObj.transform)
       {
         Destroy(item.gameObject);
       }
    }
    void destroyCards(){
         foreach (Transform item in cardsManagerObj.transform)
       {
         Destroy(item.gameObject);
       }
    }
}
