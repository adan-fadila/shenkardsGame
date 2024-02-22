using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedLibrary;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Name;
    public Text Energy;
    public Button EndTurn;
    private GameModel gameModel;
    private Client client;
    private PlayerData playerData;
    void Start()
    {
        gameModel = GameModel.getInstance();
        client = Client.getInstance();
        playerData = new PlayerData();
        setGameData();
        
    }
    private void setGameData(){
        if (gameModel.gameData.player1.PlayeId == client.playerId)
        {
            playerData = gameModel.gameData.player1;
        }
        else
        {
            playerData = gameModel.gameData.player2;
        }
        this.Name.text = $"{playerData.PlayeName}";
        this.Energy.text = $"{playerData.Energy}";

    }

}
