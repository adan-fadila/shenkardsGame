using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsManager : MonoBehaviour//, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject Card;
    private GameModel gameModel;
    private Client client;
    public GameObject selectedCard;
    private List<GameObject> instantiatedCards = new List<GameObject>();

    private RectTransform cardRectTransform;
    private Vector3 originalScale;
    public float clickedScaleFactor = 1.2f;

 
    void Start()
    {
        client = Client.getInstance();
        
       
        selectedCard = null;

        CreateInstances();

        cardRectTransform = GetComponent<RectTransform>();
        originalScale = cardRectTransform.localScale;
    }




    public void CreateInstances()
    {
        //ClearInstances();
        // Loop to create multiple instances
        for (int i = 0; i <GameController.playerData.HandCards.Count; i++)
        {
            // Instantiate the prefab at a position and rotation
            GameObject instance = Instantiate(Card, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();

            // Calculate the position based on the width of the prefab instance and its scale
            float cardWidth = rectTransform.rect.width * instance.transform.localScale.x;
            Vector3 position = new Vector3((float)(i * 1.3 * cardWidth), 0, 0);

            // Set the position of the instantiated prefab
            instance.transform.localPosition = position;

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);
            CardDisplay cardDisplay = instance.GetComponentInChildren<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.SetCardData(GameController.playerData.HandCards[i]);
            }
            else
            {
                Debug.LogWarning("Card Display component not found on the prefab.");
            }

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);

            // Optionally, you can give the instantiated GameObject a name
            instance.name = "Instance" + i;
            AddClickScript(instance);
            instantiatedCards.Add(instance);
        }
    }
    private void ClearInstances()
    {
        foreach (var card in instantiatedCards)
        {
            Destroy(card);
        }
        instantiatedCards.Clear(); // Clear the list after destroying the instances
    }
    private void AddClickScript(GameObject obj)
    {
        // Add the click script to the GameObject
        SingleInstanceClickEffect clickEffect = obj.AddComponent<SingleInstanceClickEffect>();
        clickEffect.selectedScaleFactor = clickedScaleFactor; // Set the clicked scale factor
    }
}

public class SingleInstanceClickEffect : MonoBehaviour, IPointerClickHandler
{
    public static GameObject selectedCard;
    private Vector3 originalScale;
    public float selectedScaleFactor = 1.2f;// Adjust this value to control the scale when clicked

    void Start()
    {
        // Get the RectTransform component of this instance
        originalScale = transform.localScale;
    }

     public void OnPointerClick(PointerEventData eventData)
    {
         Debug.Log("Card clicked: " + gameObject.name);
        // Deselect the previously selected card, if any
        DeselectCard();

        // Select the clicked card
        SelectCard();
    }

    private void SelectCard()
    {
        // Set the selected card to this card
        selectedCard = gameObject;

        // Scale the selected card
        selectedCard.transform.localScale = originalScale * selectedScaleFactor;
        Debug.Log("Card selected: " + gameObject.name);
    }

    public void DeselectCard()
    {
        if (selectedCard != null)
        {
            // Reset the scale of the previously selected card
            selectedCard.transform.localScale = originalScale;
            Debug.Log("Card deselected: " + selectedCard.name);
        }
        
    }
}
