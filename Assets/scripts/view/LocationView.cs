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
}
