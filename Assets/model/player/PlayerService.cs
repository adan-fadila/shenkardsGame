
using System;
using Card_package;
namespace Player_package
{
    public class PlayerService
    {
        private playerDataAccess playerData = playerDataAccess.getInstance();
        private PlayerFactory playerFactory = new PlayerFactory();
        private CardService cardService = CardService.getInstance();
        private static PlayerService instance;
        private PlayerService() { }
        public static PlayerService getInstance()
        {
            if (instance == null)
            {
                instance = new PlayerService();
            }
            return instance;

        }
        public Player GetPlayer(int id)
        {
            ICard[] cards = getPlayerCards(id);
            return playerFactory.generate(id, cards);
        }
        private ICard[] getPlayerCards(int id){
            ICard[] playerCards = new ICard[3];
            if (!playerData.hasDeck(id))
            {
                return cardService.getRandomCards(1, 1, 1);
            }

            /*****/
            /*write code --to return player deck*/
            return playerCards;
        }
        public bool validatePlayer(string username, string password)
        {
            return password == playerData.getPlayerPass(username);
        }
        public ICard[] getPlayerCards(Player player)
        {
            return player.GetCards();
        }
        public void drawCard(Player player, int num)
        {

            int[] cards = new int[num];
            ICard[] c = player.GetCards();
            for (int i = 0; i < num; i++)
            {

                var random = new Random();
                cards[i] = random.Next(0, c.Length);

            }
            ICard[] cards1 = generateCards(cards);
            for (int i = 0; i < cards1.Length; i++)
            {
                player.displayedCards.Add(cards1[i]);
            }

        }
        private ICard[] generateCards(int[] cardsId)
        {
            ICard[] cards = new ICard[cardsId.Length];
            for (int i = 0; i < cardsId.Length; i++)
            {
                cards[i] = cardService.getCard(cardsId[i]);
            }
            return cards;
        }
        public int getPlayerEnergy(Player player)
        {
            return player.energy;
        }
        public void updatePlayerEnergy(Player player, int en)
        {
            player.energy += en;
        }
    }
}