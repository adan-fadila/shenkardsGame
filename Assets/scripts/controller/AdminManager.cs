using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    public InputField id;
    public InputField power;
    public InputField energy;
    public Button SaveButton;
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
        OnSaveEditClick();
    }
    public void OnLogOutClick()
    {
        client.Close();
        SceneManager.LoadScene("Login");
    }

    public void OnSaveEditClick() 
    {
        client.AdminUpdateCard(1, 2, 5);
    }
}
