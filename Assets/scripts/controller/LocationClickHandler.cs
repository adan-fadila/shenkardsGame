using UnityEngine;
using UnityEngine.EventSystems;

public class LocationClickHandler : MonoBehaviour, IPointerClickHandler
{
     public GameObject playerZone; // Reference to the play zone GameObject

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if a card is selected
        if (SingleInstanceClickEffect.selectedCard != null)
        {
            // Get the selected card's transform
            Transform selectedCardTransform = SingleInstanceClickEffect.selectedCard.transform;

            // Get the position of the play zone in the clicked location
            Vector3 targetPosition = GetPlayZonePosition(eventData.pointerPressRaycast.worldPosition);

            // Move the selected card to the play zone position
            selectedCardTransform.position = targetPosition;
            
            // Deselect the card
            SingleInstanceClickEffect.selectedCard.GetComponent<SingleInstanceClickEffect>().DeselectCard();
        }
    }

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