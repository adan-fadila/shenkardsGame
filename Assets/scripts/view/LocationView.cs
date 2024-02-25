using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player_package;
using Location_package;

public class LocationView : MonoBehaviour
{
    ILocation[] locations;
    ILocation location;
    LocationController locationController = new LocationController();

    private Sprite image;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image locationImage;
    public TextMeshProUGUI ScoreText;
    public bool revealed;

    void Start()
    {
        locations = locationController.getLocations(1);
        location = locations[0];
        setLocation(location);


    }

    void Update()
    {
        
    }

    void setLocation(ILocation location)
    {
        nameText.text = location.Name;
        descriptionText.text = location.Desc;
        image = Resources.Load<Sprite>("locationImages/" + location.Image);
        locationImage.sprite = image;
        Debug.Log(image);
        Debug.Log(location.Image);


    }
    public void updateScore(int cardPower)
    {
        if (location != null)
        {
            // Update the location's score
            location.Score += cardPower;

            // Update the ScoreText UI element to reflect the new score
            ScoreText.text = "Score: " + location.Score.ToString();

            // Optionally, log the updated score for debugging
            Debug.Log("Updated location score: " + location.Score);
        }
        else
        {
            Debug.LogError("No location set in LocationView to update the score.");
        }
    }


}
