using SharedLibrary;
public class GameModel
{
    private static GameModel instance;
    public GameData gameData;
    private GameModel(){
        gameData = null;
    }
    public static GameModel getInstance(){
        if(instance == null){
            instance = new GameModel();
        }
        return instance;
    }
}