using System;
using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class LocationDisplay : MonoBehaviour
{
    public Text name1;
    public Text description;
    public Image LocationImage;
    public Text PlayerScore;
    public Text oppScore;

  
    // Start is called before the first frame update
    public void SetLocationData(LocationData location, GameData gameData,Client client)
    {
        if (gameData == null)
        {
            Debug.Log("location null");
            return;
        }
        // Set the name and description text of the card
        name1.text = location.Name;
        description.text = location.Desc;
        if(gameData == null || gameData.player1 == null|| client == null){
            Debug.Log("null player1");
        }

        try
        {
            if (gameData.player1.PlayeId == client.playerId)
            {
                PlayerScore.text = $"{location.Player1LocatinScore}";
                oppScore.text = $"{location.Player1LocatinScore}";
            }
            else
            {
                PlayerScore.text = $"{location.Player2LocatinScore}";
                oppScore.text = $"{location.Player1LocatinScore}";
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }
}
