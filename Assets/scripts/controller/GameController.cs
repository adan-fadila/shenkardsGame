using System.Collections.Generic;
using Card_package;
using Game_package;
using Location_package;
using Player_package;

class GameController
{
    private Game game;
    private GameService gameService = new GameService();
 
    public void putCardToLocation(Player player, Location location, ICard card)
    {
        gameService.putCardToLocation(player, location, card, game);

    }

    // public void endTurn()
    public Game askForGame(int id)
    {
        this.game = gameService.askForGame(id);
        return game;
    }
    public List<int> startBattle()
    {
        return gameService.startBattle(game);
    }
}