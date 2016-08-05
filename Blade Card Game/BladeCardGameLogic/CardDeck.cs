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
            //for loop to iterated and create each card up until the max card value. 
            for (byte value = 1; value <= ValueCards.MAX_CARD_VALUE; value++)
            {
                ValueCards valCard = new ValueCards(value);

                _cardList.Add(valCard);
            }
        }


        //method for shuffling cards
        private void ShuffleCards()
        {
            for (byte iDeckPos = 0; iDeckPos < _cardList.Count; iDeckPos++)
            {

                //take a random card out of the deck and place it in a high pos
                int randIndex = Randomizer.Next(iDeckPos, _cardList.Count);

                //swap cards randomly 
                ValueCards crtCard = _cardList[iDeckPos];
                _cardList[iDeckPos] = _cardList[randIndex];
                _cardList[randIndex] = crtCard;
            }
        }
    }
}
