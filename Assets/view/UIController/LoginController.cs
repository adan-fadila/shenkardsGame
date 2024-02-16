using System.Collections;
using System.Collections.Generic;
using Player_package;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    PlayerController playerController = new PlayerController();
    public InputField nameInputField;
    public InputField passInputField;
    public Button logInButton;
    public void onLoginClick()
    {
        if (playerController.validatePlayer(nameInputField.text.ToString(), passInputField.text.ToString()))
        {
            Debug.Log("yes");
        }
        else
        {
            Debug.Log("no");
        }
    }

}

