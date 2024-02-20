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


    // void Start()
    // {
    //     Player player = playerController.GetPlayer(1);
    //     thisCard = playerController.getPlayerCards(player);
    // }
   public void setCard(ICard card)
    {
        id = card.id;
        cardName = card.Name;
        power = card.Power;
        cost = card.Cost;
        cardDescription = card.Desc;

        nameText.text = "" + cardName;
        descText.text = "" + cardDescription;
        powerText.text = "" + power;
        costText.text = "" + cost;
    }
}
