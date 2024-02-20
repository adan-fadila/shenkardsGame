// using System.Collections;
// using System.Collections.Generic;
// using Player_package;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using Mirror;


// public class LoginController : NetworkBehaviour
// {
//     // public static int playerId = -1;
//     // public static string PlayerName = "";
//     // PlayerController playerController = new PlayerController();
//     public InputField nameInputField;
//     public InputField passInputField;
//     public Button logInButton;
//     public void onLoginClick()
//     {
//         Debug.Log("onLogin");
//         // PlayerName = nameInputField.text.ToString();
//         try
//         {
            
//            CmdLogin(nameInputField.text, passInputField.text);
//         }
//         catch (System.Exception)
//         {

//             Debug.LogError("vaildate player err.");
//         }

//     }

//     [Command]
//     private void CmdLogin(string username, string password)
//     {
//         // Server-side authentication logic goes here
//     }

// }

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;

    private Client client;

    void Start()
    {
        // Assuming you have a Client component attached to the same GameObject
        client = Client.getInstance();
        
    }

    public void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Call the SendLoginInfo method of the client component
            client.Login(username, password);
        }
        else
        {
            Debug.LogError("Username and password cannot be empty!");
        }
        SceneManager.LoadScene("MainMenu");
    }
}
