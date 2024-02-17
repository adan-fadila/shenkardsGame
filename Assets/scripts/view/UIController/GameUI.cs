using System.Collections;
using System.Collections.Generic;
using Game_package;
using Player_package;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI instance;
    private GameUI(){}
    public static GameUI getInstance(){
        if(instance == null){
            instance = new GameUI();
        }
        return instance;
    }
    public Text playerName;
    public Text oppName;
    public void UpdateGame(Game game){
        if(game == null){
            playerName.text = "me";
            oppName.text = "him";
        }
        foreach (Player p in game.Players)
        {
            if(p.id == PlayerName.getPlayerId()){
                playerName.text = p.name;
            }
            else {
                oppName.text = p.name;
            }
        }
    }

}
