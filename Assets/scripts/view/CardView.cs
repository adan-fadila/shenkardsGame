using System.Collections;
using System.Collections.Generic;
using Card_package;
using Player_package;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    PlayerController playerController = new PlayerController();
    public ICard[] thisCard;
    public int thisId;
    public int id;
    public string cardName;
    public int power;
    public int cost;
    public string cardDescription;

    public Text nameText;
    public Text descText;
    public Text costText;
    public Text powerText;


    void Start()
    {
        Player player = playerController.GetPlayer(1);
        thisCard = playerController.getPlayerCards(player);
    }
    void Update()
    {
        id = thisCard[1].id;
        cardName = thisCard[1].Name;
        power = thisCard[1].Power;
        cost = thisCard[0].Cost;
        cardDescription = thisCard[0].Desc;

        nameText.text = "" + cardName;
        descText.text = "" + cardDescription;
        powerText.text = "" + power;
        costText.text = "" + cost;
    }
}
