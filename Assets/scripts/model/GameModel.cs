using SharedLibrary;

class GameModel
{
    private static GameModel instance;
    private GameModel(){
        GameData gameData = new GameData();
    }
    public static GameModel getInstance(){
        if(instance == null){
            instance = new GameModel();
        }
        return instance;
    }
}