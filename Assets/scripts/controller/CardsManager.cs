using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Card;
    private GameModel gameModel;
    private Client client;
    private PlayerData playerData;
    public GameObject selectedCard;

    void Start()
    {
        client = Client.getInstance();
        playerData = new PlayerData();
        gameModel = GameModel.getInstance();
        if (gameModel.gameData.player1.PlayeId == client.playerId)
        {
            playerData = gameModel.gameData.player1;
        }
        else
        {
            playerData = gameModel.gameData.player2;
        }
        // Call the function to create instances of the prefab
        CreateInstances();
    }
    void Update()
    {
        // Check for mouse click on a card
        if (Input.GetMouseButtonUp(0)) // Left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Card"))
                {
                    // Select the clicked card
                    selectedCard = clickedObject;
                }
            }
        }

        if (selectedCard != null && Input.GetMouseButtonUp(0)) // Left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                // Check if the clicked location is valid (e.g., a drop zone)
                if (clickedObject.CompareTag("DropZone"))
                {
                    // Place the selected card at the clicked location
                    selectedCard.transform.position = hit.point;
                    // Clear the selection
                    selectedCard = null;
                }
            }
        }
    }

    void CreateInstances()
    {

        // Loop to create multiple instances
        for (int i = 0; i < playerData.HandCards.Count; i++)
        {
            // Instantiate the prefab at a position and rotation
            GameObject instance = Instantiate(Card, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();

            // Calculate the position based on the width of the prefab instance and its scale
            float cardWidth = rectTransform.rect.width * instance.transform.localScale.x;
            Vector3 position = new Vector3(i * cardWidth, 0, 0);

            // Set the position of the instantiated prefab
            instance.transform.localPosition = position;

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);
            CardDisplay cardDisplay = instance.GetComponentInChildren<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.SetCardData(playerData.HandCards[i]);
            }
            else
            {
                Debug.LogWarning("Card Display component not found on the prefab.");
            }

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);

            // Optionally, you can give the instantiated GameObject a name
            instance.name = "Instance" + i;
        }
    }
}
