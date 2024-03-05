using System;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Transform playerZone; // Reference to the play zone GameObject
    int num = 0;
    public LocationData locationData1;
    // Define a variable to store the previously selected card

    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            if (SingleInstanceClickEffect.selectedCard != null && playerZone != null)
            {
                CardDisplay customScript = SingleInstanceClickEffect.selectedCard.GetComponent<CardDisplay>();
                if (customScript != null)
                {
                    CardData selectedCardData = new CardData{
                        id = customScript.id,
                        Name = customScript.nameLabel.text,
                        Desc = customScript.descriptionLabel.text,
                        Image = customScript.imgName,
                        Cost = int.Parse(customScript.cost.text),
                        Power = int.Parse(customScript.power.text)
                    };
                    if(GameController.playerData.Energy <selectedCardData.Cost){
                        return;
                    }
                    GameController.playerData.Energy -= selectedCardData.Cost;
                    
                    // GameController.Energy.text = GameController.playerData.Energy.ToString();
                    // Access data from the custom script
                    PlayedCardsModel.playedCards.Add(new PlayedCard(){
                        cardData = selectedCardData,
                        locationData = locationData1
                    });
                }






                RectTransform selectedCardRectTransform = SingleInstanceClickEffect.selectedCard.GetComponent<RectTransform>();

                // Get the RectTransform of the player zone
                RectTransform playerZoneRectTransform = playerZone.GetComponent<RectTransform>();
                if(playerZoneRectTransform.childCount == 4){
                    return;
                }

                selectedCardRectTransform.SetParent(playerZoneRectTransform, false);

                selectedCardRectTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

                
                SingleInstanceClickEffect scriptToRemove = SingleInstanceClickEffect.selectedCard.GetComponent<SingleInstanceClickEffect>();
                SingleInstanceClickEffect.selectedCard = null;
                // Check if the script exists
                if (scriptToRemove != null)
                {
                    // Remove the script component from the selected card GameObject
                    Destroy(scriptToRemove);
                }
                
                

                RearrangePlayerZoneWithPadding(playerZone);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error handling click: {e}");
        }
    }

    public static void RearrangePlayerZoneWithPadding(Transform playerZone)
    {
        if (playerZone == null)
        {
            Debug.LogWarning("Player zone panel is not assigned.");
            return;
        }

        // Ensure there is at least one child in the player zone
        if (playerZone.transform.childCount < 1)
        {
            return;
        }

        // Get the RectTransform of the player zone
        RectTransform playerZoneRectTransform = playerZone.GetComponent<RectTransform>();

        // Calculate the total padding (in pixels) between each child
        float padding = 10f; // Adjust this value as needed

        // Get the number of child objects in the player zone
        int childCount = playerZoneRectTransform.childCount;

        // Calculate the total width of all child objects (including padding)
        float totalWidth = (playerZoneRectTransform.sizeDelta.x - (padding * (childCount - 1)));

        // Calculate the width of each child (assuming equal distribution)
        float childWidth = totalWidth / childCount;

        // Loop through each child object and set its position and size
        for (int i = 0; i < childCount; i++)
        {
            RectTransform childRectTransform = playerZoneRectTransform.GetChild(i).GetComponent<RectTransform>();

            // Calculate the x position of the child
            float xPos = (childWidth + padding) * i - 100;

            // Set the anchored position of the child
            childRectTransform.anchoredPosition = new Vector2(xPos, 0);

            // Set the size of the child
            childRectTransform.sizeDelta = new Vector2(childWidth, playerZoneRectTransform.sizeDelta.y);
        }
    }

    // Call this method whenever you need to rearrange the child objects within the player zone panel



    private Vector3 GetPlayZonePosition(Vector3 clickedPosition)
    {
        // Get the position of the play zone in the clicked location
        // You may need to adjust this logic based on how the play zone is positioned relative to the location
        if (playerZone == null)
        {
            Debug.LogError("playerZone is null!");
            return Vector3.zero; // Return a default position or handle the error gracefully
        }
        else
        {
            // Return the position of the player zone
            return playerZone.transform.position;
        }
    }
}