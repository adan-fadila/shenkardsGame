using SharedLibrary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class CardDisplay : MonoBehaviour
{
    public TMP_Text nameLabel;
    public TMP_Text descriptionLabel;
    public Image cardImage;
    public TMP_Text cost;
    public TMP_Text power;
    public void SetCardData(CardData card)
    {
        // Set the name and description text of the card
        nameLabel.text = card.Name;
        descriptionLabel.text = card.Desc;
        cost.text = $"{card.Cost}";
        power.text = $"{card.Power}";

        // Set the image of the card (assuming the card has a sprite)
        if (card.Image != null)
        {
            Debug.Log(card.Image);
            cardImage.sprite = Resources.Load<Sprite>($"cardImages/{card.Image}") as Sprite;
            // cardImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("img is null");
            // If no image is available, hide the image component
            cardImage.gameObject.SetActive(false);
        }
    }
}