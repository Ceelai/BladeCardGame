using BladeCardGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    public class CardDeck
    {


        //using a list to gather cards 
        private List<ValueCards> _cardList;



        //field variable for the static card randomizer
        private static Random s_randomizer;



        /// <summary>
        /// Static constructor for the statice randomizer 
        /// </summary>
        static CardDeck()
        {
            s_randomizer = new Random();

        }



        /// <summary>
        /// Static read-only property
        /// </summary>
        public static Random Randomizer
        {
            get { return s_randomizer; }
        }



        //Define a constructor the Game class to create the deck
        public CardDeck()
        {
            //create the card deck object
            _cardList = new List<ValueCards>();

            //add the cards to the deck
            CreateCards();
        }

        private void CreateCards()
        {

        }
    }
}
