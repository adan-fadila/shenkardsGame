using System.Collections;
using System.Collections.Generic;
using Card_package;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardTemplate;
    public Transform cardParent; // Parent transform for instantiated cards
    private PlayerController playerController = new PlayerController();
    private ICard[] playerCards; // Assume this list contains player's card data

    void Start()
    {
        // Example: Assuming playerCards list is populated from some data source

        playerCards = playerController.drawCard(LoginController.player, 3);

        // Instantiate cards for each card data
        foreach (ICard card in playerCards)
        {
            GameObject newCard = Instantiate(cardTemplate, cardParent);
            // Customize card appearance and data
            newCard.GetComponent<CardView>().setCard(card);
        }
    }
}

