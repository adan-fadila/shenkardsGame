using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<CardData> cardDatas = new List<CardData>();
    Client client;
    void Start(){
        client = Client.getInstance();
    }
    public void OnEditCardsClick()
    {
        cardDatas = client.AdminGetCards();
        SceneManager.LoadScene("EditCards");
    }
    public void OnLogOutClick()
    {
        client.Close();
        SceneManager.LoadScene("Login");
    }
}
