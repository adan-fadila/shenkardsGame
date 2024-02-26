using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedLibrary;
using UnityEngine.UI;
using System;
public class GameController : MonoBehaviour
{
    public Text Name;
    public Text Energy;
    public Button EndTurn;
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
        while (gameModel.gameData == null)
        {
            yield return null;
        }
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
