using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationManager : MonoBehaviour
{
    public GameObject Location;

    private GameModel gameModel;
    private Client client;
    // Start is called before the first frame update
    void Awake()
    {
        client = Client.getInstance();
        gameModel = GameModel.getInstance();
        CreateInstances();
    }



    public void CreateInstances()
    {
        // Loop to create multiple instances
        for (int i = 0; i < gameModel.gameData.locationDatas.Count; i++)
        {
            // Instantiate the prefab at a position and rotation
            GameObject instance = Instantiate(Location, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();

            // Calculate the position based on the width of the prefab instance and its scale
            float Locationwidth = rectTransform.rect.width * instance.transform.localScale.x;
            Vector3 position = new Vector3((float)(i * 3 * Locationwidth), 0, 0);

            // Set the position of the instantiated prefab
            instance.transform.localPosition = position;

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);



            Transform playerZone = instance.transform.Find("border/playerZone");


            AddClickScript(instance, playerZone,gameModel.gameData.locationDatas[i]);
         
            LocationDisplay locationDisplay = instance.GetComponentInChildren<LocationDisplay>();
            if (locationDisplay != null)
            {
               
                locationDisplay.SetLocationData(gameModel.gameData.locationDatas[i], gameModel.gameData, client);
            }
            else
            {
                Debug.LogWarning("Card Display component not found on the prefab.");
            }

            // Optionally, you can give the instantiated GameObject a name
            instance.name = "Instance" + i;

        }
    }
       private void AddClickScript(GameObject obj, Transform playerZone,LocationData locationData)
    {
        // Add the click script to the GameObject
        LocationClickHandler clickEffect = obj.AddComponent<LocationClickHandler>();
        clickEffect.playerZone = playerZone;
        clickEffect.locationData1 = locationData;
        Debug.Log("PLayerZone: " + playerZone);

    }
    private GameObject GetPlayerZoneForInstance(int index)
    {
        // Your logic to determine the player zone for each instance
        // For example, if you have an array of player zones:
        GameObject[] playerZones = GameObject.FindGameObjectsWithTag("DropZone");

        // Ensure the index is within the range of player zones
        if (index >= 0 && index < playerZones.Length)
        {
            return playerZones[index];
        }
        else
        {
            Debug.LogError($"Player zone for instance {index} not found!");
            return null;
        }

    }
}

