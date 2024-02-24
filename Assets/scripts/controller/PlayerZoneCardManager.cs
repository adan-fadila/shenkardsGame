using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;

class PlayerZoneCardManager : MonoBehaviour
{
    public GameObject Card;

      void CreateInstances(List<CardData> cardDatas)
    {

        // Loop to create multiple instances
        for (int i = 0; i < cardDatas.Count; i++)
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
                cardDisplay.SetCardData(cardDatas[i]);
            }
            else
            {
                Debug.LogWarning("Card Display component not found on the prefab.");
            }

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);

            // Optionally, you can give the instantiated GameObject a name
            instance.name = "Instance" + i;
        }}
}