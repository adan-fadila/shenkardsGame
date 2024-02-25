using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Location_package;

public class LocationSlots : MonoBehaviour, IDropHandler
{
    public LocationView locationView;
    public void OnDrop(PointerEventData eventData)
    {

        // Access the LocationService instance directly
       Location_package.LocationService locationService = Location_package.LocationService.getInstance();
        CardView cardView = eventData.pointerDrag.GetComponent<CardView>();
        if (eventData.pointerDrag != null)
        {
            int cardPower = cardView.getPower();
            locationView.updateScore(cardPower);

            RectTransform draggedItemRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform zoneRectTransform = GetComponent<RectTransform>();

            // Make the dragged item a child of the zone
            draggedItemRectTransform.SetParent(zoneRectTransform, false);

            // Assuming all cards have the same width and adding a small space between cards
            float cardWidth = draggedItemRectTransform.rect.width;
            float spacing = 10f; // Adjust the spacing between cards as needed

            // Calculate the position of the new card based on how many cards are already in the zone
            int cardCount = zoneRectTransform.childCount - 1; // Subtract 1 because the new card is already a child
            float newXPosition = -(zoneRectTransform.rect.width / 2) + (cardWidth / 2) + (cardCount * (cardWidth + spacing));

            // Set the new card's position
            draggedItemRectTransform.anchoredPosition = new Vector2(newXPosition, 0);
        }
    }
}
