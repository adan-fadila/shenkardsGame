using Card_package;
using Player_package;

class PlayerController
{
    private PlayerService playerService = PlayerService.getInstance();
    public int getPlayerEnergy(Player player)
    {
        return playerService.getPlayerEnergy(player);
    }
    public void updatePlayerEnergy(Player player, int en)
    {
        playerService.updatePlayerEnergy(player, en);
    }
    public void drawCard(Player player, int num)
    {

        playerService.drawCard(player, num);
    }
    public ICard[] getPlayerCards(Player player)
    {
        return playerService.getPlayerCards(player);
    }
    public bool validatePlayer(string username, string password){
        return playerService.validatePlayer(username,password);
    }
    public Player GetPlayer(int id){
        return playerService.GetPlayer(id);
    }
}