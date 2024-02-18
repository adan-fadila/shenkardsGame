using System.Collections;
using System.Collections.Generic;
using Card_package;
using Player_package;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    PlayerController playerController = new PlayerController();
    CardController cardController = new CardController();  
    ICard card;
    private ICard[] thisCard;
    private int thisId;
    private int id;
    private string cardName;
    private int power;
    private int cost;
    private string cardDescription;
    private Sprite image;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI powerText;
    public Image cardImage;
    public string cardImageString;

    void Awake()
    {
        card = cardController.getCard(2);
    }
    void Start()
    {
        setCard(card);
    }
    public void setCard(ICard card)
    {
        id = card.id;
        cardName = card.Name;
        power = card.Power;
        cost = card.Cost;
        cardDescription = card.Desc;
        cardImageString = card.Image;

        nameText.text = "" + cardName;
        descText.text = "" + cardDescription;
        powerText.text = "" + power;
        costText.text = "" + cost;
        image = Resources.Load<Sprite>("cardImages/" + cardImageString);
        cardImage.sprite = image;
    }
}
    