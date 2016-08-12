using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    class Deck
    {
        private Cards[] deck;
        private int currentCard;
        private const int NUMBER_OF_CARDS = 12;
        private Random randNum;

        public Deck()
        {
            string[] values = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Mirror", "Bolt" };
            deck = new Cards[NUMBER_OF_CARDS];
            currentCard = 0;
            randNum = new Random();

            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = new Cards(values[i / 12]);
            }
        }

        public void ShuffleDeck()
        {
            currentCard = 0;

            for (int i = 0; i < deck.Length; i++)
            {
                int e = randNum.Next(NUMBER_OF_CARDS);
                Cards temp = deck[i];
                deck[i] = deck[e];
                deck[e] = temp;
            }
        }

        public Cards DealCard()
        {
            if (currentCard < deck.Length)
            {
                return deck[currentCard++];
            }
            else
            {
                return null;
            }
        }
    }
}
