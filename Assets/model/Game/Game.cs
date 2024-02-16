using System.Collections.Generic;
using Card_package;
using Location_package;
using Player_package;
using UnityEngine;
namespace Game_package
{
  public class Game
  {
    private const int numOfTurns = 6;
    public List<Player> Players { get; private set; }

    public ILocation[] locations { get; private set; }
    private int turn;

    private IBattleStrategy battleStrategy;
    [SerializeField]
    public Game(Player player1, Player player2, IBattleStrategy battleStrategy, Location[] locations)
    {
      this.battleStrategy = battleStrategy;
      Players = new List<Player>();
      Players.Add(player2);
      Players.Add(player1);
      this.locations = locations;
      this.turn = 0;
    }
    public bool endTurn()
    {
      if (this.turn < numOfTurns){

         this.turn++;
         return true;
      }
       return false;
    }

    public List<int> getWinner()
    {

      return this.battleStrategy.battle(this);
    }


  }
}
