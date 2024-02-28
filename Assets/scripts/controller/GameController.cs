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
    public Text EndGameText;
    public Button EndTurn;
    private GameModel gameModel;
    private Client client;
    public static PlayerData playerData;
    public GameObject cardsManagerObj;
    public GameObject locationsManagerObj;
    public GameObject endGameScreen;
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
        }
        else
        {
            playerData = gameModel.gameData.player2;
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
            Debug.Log("gameEnd");
        }
        else
        {
            if (gameModel.winners.Count > 0)
            {
                if(gameModel.winners.Contains(client.playerId))
                {
                    EnableEndGameScreen();
                    Debug.Log("You have won the game!");
                    EndGameText.text = "You have won the game!";
                }
                else
                {
                    EnableEndGameScreen();
                    Debug.Log("You have lost the game!");
                    EndGameText.text = "You have lost the game!";

                }
            }
            else
            {
                setGameData();

                if (cardsManager != null)
                {
                    cardsManager.CreateInstances();
                }
                if (locationManager != null)
                {
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
    void EnableEndGameScreen()
    {
        endGameScreen.SetActive(true);
    }

    void DisableEndGameScreen()
    {
        endGameScreen.SetActive(false);
    }
}
