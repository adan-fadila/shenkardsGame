

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
