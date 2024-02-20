using System.Collections;
using System.Collections.Generic;
using SharedLibrary;
using UnityEngine;

public class GameModel
{
    private static GameModel instance;
    public GameData gameData;
    private GameModel(){
        gameData = new GameData();
    }
    public static GameModel getInstance(){
        if(instance == null){
            instance = new GameModel();
        }
        return instance;
    }

}
