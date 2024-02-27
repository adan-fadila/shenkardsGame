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
    public Text TimerText;
    private GameModel gameModel;
    private Client client;
    public static PlayerData playerData;
    public GameObject cardsManagerObj;
    public GameObject locationsManagerObj;

    private bool endTurnClicked = false; // Flag to track if end turn has been manually clicked
    private Coroutine endTurnCountdownCoroutine; // Reference to the countdown coroutine
    private float countdownTime = 10f;

    void Awake()
    {
        gameModel = GameModel.getInstance();
        client = Client.getInstance();
        playerData = new PlayerData();
        setGameData();
        ResetEndTurnTimer(); // Initialize the end turn timer
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
        if (!endTurnClicked) // Check if the button hasn't been clicked before
        {
            EndTurnHandler();
        }
    }

    private void EndTurnHandler()
    {
        client.EndTurn(PlayedCardsModel.playedCards, gameModel);
        gameModel.gameData = null;
        EndTurn.interactable = false;
        endTurnClicked = true; // Set the flag to true after manual click
        if (endTurnCountdownCoroutine != null)
        {
            StopCoroutine(endTurnCountdownCoroutine); // Stop the current countdown
        }
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
        endTurnClicked = false; // Reset for next turn
        ResetEndTurnTimer(); // Reset the timer for the next turn
    }

    void ResetEndTurnTimer()
    {
        if (endTurnCountdownCoroutine != null)
        {
            StopCoroutine(endTurnCountdownCoroutine); // Ensure any existing countdown is stopped
        }
        endTurnCountdownCoroutine = StartCoroutine(EndTurnCountdown()); // Start a new countdown
    }

    IEnumerator EndTurnCountdown()
    {
        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            TimerText.text = $"{Mathf.CeilToInt(remainingTime)}"; // Update the timer text
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        TimerText.text = "0"; // Set timer to 0 when countdown finishes

        if (!endTurnClicked) // Automatically trigger end turn if it hasn't been clicked manually
        {
            EndTurnHandler();
        }
    }
}
