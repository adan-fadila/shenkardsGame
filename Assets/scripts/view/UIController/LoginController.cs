using System.Collections;
using System.Collections.Generic;
using Player_package;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoginController : MonoBehaviour
{
    public static Player player;
    PlayerController playerController = new PlayerController();
    public InputField nameInputField;
    public InputField passInputField;
    public Button logInButton;
    public  void onLoginClick()
    {
        if (playerController.validatePlayer(nameInputField.text.ToString(), passInputField.text.ToString()))
        {
            player = playerController.GetPlayer(nameInputField.text.ToString());
            SceneManager.LoadScene("MainMenu");
            
        }
        else
        {
            Debug.Log("no");
        }
    }

}

