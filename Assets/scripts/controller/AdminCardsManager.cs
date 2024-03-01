using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCardsManager : MonoBehaviour
{
    public GameObject Card;
    // Start is called before the first frame update
    void Start()
    {
        CreateInstances();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateInstances()
    {

        // Loop to create multiple instances
        for (int i = 0; i <AdminManager.cardDatas.Count; i++)
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
                cardDisplay.SetCardData(AdminManager.cardDatas[i]);
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
