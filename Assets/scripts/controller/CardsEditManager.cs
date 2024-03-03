using UnityEngine;
using UnityEngine.UI;

public class CardEditManager : MonoBehaviour
{
    public InputField idInputField;
    public InputField powerInputField;
    public InputField costInputField;
    public Button updateButton;

    void Start()
    {
        updateButton.onClick.AddListener(OnUpdateButtonClick);
    }

    private void OnUpdateButtonClick()
    {
        int id = int.Parse(idInputField.text);
        int power = int.Parse(powerInputField.text);
        int cost = int.Parse(costInputField.text);

        UpdateCard(id, power, cost);
    }

    private void UpdateCard(int cardId, int newPower, int newCost)
    {
        Client.getInstance().AdminUpdateCard(cardId, newCost, newPower);

    }
}
