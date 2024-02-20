using System.Collections;
using System.Collections.Generic;
using Card_package;
using JetBrains.Annotations;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject card;
    public GameObject handArea;


    private void Start()
    {

        GameObject playerCard = Instantiate(card, new Vector3(0,0,0), Quaternion.identity);
        playerCard.transform.SetParent(handArea.transform, false);
    }
}

