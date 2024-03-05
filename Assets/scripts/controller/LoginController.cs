

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
            if (client.Login(username, password))
            {
                SceneManager.LoadScene("MainMenu");
                return;
            }
            ShowText("username or password are invalid");

        }
        else
        {
            
            ShowText("Username and password cannot be empty!");
        }

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











    public void onSignUpButtonClick(){
        SceneManager.LoadScene("SignUp");
    }
      public void onBackButtonClick(){
        SceneManager.LoadScene("Login");
    }
      public void onSignUpClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Call the SendLoginInfo method of the client component
            if (client.SignUp(username,password))
            {
                SceneManager.LoadScene("Login");
                return;
                
            }
            ShowText("this name is already exist try another");

        }
        

    }
}
