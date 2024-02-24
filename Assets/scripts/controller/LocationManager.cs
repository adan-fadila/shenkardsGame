using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationManager : MonoBehaviour
{
    public GameObject Location;
    public GameObject playerZone;
    private GameModel gameModel;
    private Client client;
    private PlayerData playerData;
    // Start is called before the first frame update
    void Awake()
    {
        client = Client.getInstance();
        gameModel = GameModel.getInstance();
        CreateInstances();
    }
    void Start(){
        LocationClickHandler[] locationHandlers = FindObjectsOfType<LocationClickHandler>();

        // Assign the player zone to the playZone variable in each LocationClickHandler script
        foreach (LocationClickHandler handler in locationHandlers)
        {
            Debug.Log("1");
            handler.playerZone = playerZone;
        }
    }

    void CreateInstances()
    {
        // Loop to create multiple instances
        for (int i = 0; i < gameModel.gameData.locationDatas.Count; i++)
        {
            // Instantiate the prefab at a position and rotation
            GameObject instance = Instantiate(Location, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();

            // Calculate the position based on the width of the prefab instance and its scale
            float Locationwidth = rectTransform.rect.width * instance.transform.localScale.x;
            Vector3 position = new Vector3(i * Locationwidth, 0, 0);

            // Set the position of the instantiated prefab
            instance.transform.localPosition = position;

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);
            LocationDisplay locationDisplay = instance.GetComponentInChildren<LocationDisplay>();
            if (locationDisplay != null)
            {
                if (gameModel.gameData.locationDatas[i] == null)
                {
                    Debug.Log("location is null");
                    return;
                }
                locationDisplay.SetLocationData(gameModel.gameData.locationDatas[i], gameModel.gameData, client);
            }
            else
            {
                Debug.LogWarning("Location Display component not found on the prefab.");
            }

            // Set the parent using the SetParent method
            instance.transform.SetParent(transform, false);

            // Optionally, you can give the instantiated GameObject a name
            instance.name = "Instance" + i;
            AddClickScript(instance);
        }
    }
    private void AddClickScript(GameObject obj)
    {
        // Add the click script to the GameObject
        LocationClickHandler clickEffect = obj.AddComponent<LocationClickHandler>();
        clickEffect.playerZone = playerZone;
        // clickEffect.selectedScaleFactor = clickedScaleFactor; // Set the clicked scale factor
    }
}

