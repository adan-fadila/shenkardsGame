using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********************************/
/*JUST FOR test need to delete*/
/******************************/
namespace Card_package
{
    public class main : MonoBehaviour
    {
        public static void Main()
        {
            CardService cardService = CardService.getInstance();
            
            List<ICard> cards = new List<ICard>();
            cards = cardService.getCards();
            foreach(ICard card in cards){
                Debug.Log(card.Desc);
                if(card is AbilityCard abilityCard){
                    abilityCard.ActivateAbility();
                }
                
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            Main();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}