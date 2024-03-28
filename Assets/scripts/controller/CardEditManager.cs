//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections; // Required for coroutines

//public class CardEditManager : MonoBehaviour
//{
//    public InputField idInputField;
//    public InputField powerInputField;
//    public InputField costInputField;
//    public Button updateButton;

//    void Start()
//    {
//        updateButton.onClick.AddListener(OnUpdateButtonClick);
//    }

//    private void OnUpdateButtonClick()
//    {
//        int id = int.Parse(idInputField.text);
//        int power = int.Parse(powerInputField.text);
//        int cost = int.Parse(costInputField.text);

//        StartCoroutine(UpdateCard(id, power, cost)); // Start the coroutine
//    }

//    // Adjusted to be a coroutine
//    private IEnumerator UpdateCard(int cardId, int newPower, int newCost)
//    {
//        yield return Client.getInstance().AdminUpdateCard(cardId, newCost, newPower);
//        // After the card update completes, you might want to refresh the UI or show a confirmation message here.
//    }
//}
