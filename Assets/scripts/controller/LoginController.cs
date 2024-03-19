

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Text textComponent;
    public Button back;
    public Button Admin;

    private Client client;

    void Awake()
    {
        HideText();
    }
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
            StartCoroutine(LoginCoroutine());
        }
        else
        {

            ShowText("Username and password cannot be empty!");
        }
    }

    IEnumerator LoginCoroutine()
    {
        // Wait until the game data is received from the server
        while (client.playerId == -1 || client.playerId == -2)
        {
            if (client.playerId == -1)
                ShowText("username or password are invalid");
            yield return null;
        }
        // Once game data is received, proceed to the game scene

        SceneManager.LoadScene("MainMenu");





    }












    public void ShowText(string text)
    {
        // Enable the GameObject containing the Text component
        textComponent.text = text;
        textComponent.gameObject.SetActive(true);
    }

    // Function to hide the text
    public void HideText()
    {
        // Disable the GameObject containing the Text component
        textComponent.gameObject.SetActive(false);
    }










    public void onSignUpButtonClick()
    {
        SceneManager.LoadScene("SignUp");
    }
    public void onBackButtonClick()
    {
        SceneManager.LoadScene("Login");
    }
    public void onSignUpClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Call the SendLoginInfo method of the client component
            client.SignUp(username, password);
            StartCoroutine(SignUpCoroutine());

        }


    }
    IEnumerator SignUpCoroutine()
    {
        // Wait until the game data is received from the server
        while (!client.signup)
        {
        
             ShowText("username or password are invalid");
            yield return null;
        }
        // Once game data is received, proceed to the game scene

        SceneManager.LoadScene("Login");





    }

}
