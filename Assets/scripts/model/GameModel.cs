using System.Collections.Generic;
using SharedLibrary;
public class GameModel
{
    private static GameModel instance;
    public GameData gameData;
    public bool gameEnd = false;
    public List<int> winners = new List<int>();
    private GameModel(){
        gameData = null;
    }
    public static GameModel getInstance(){
        if(instance == null){
            instance = new GameModel();
        }
        return instance;
    }
    void setGameData(GameData gameData){
        this.gameData = gameData;
    }
}