    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    public class Deck
    {
        private List<Cards> cards;


        public Deck()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            cards = new List<Cards>();

            for (int i = 0; i < 12; i++)
            {
                cards.Add(new Cards() { Face = (Face)i });

                if (i <= 11)
                    cards[cards.Count - 1].Value = 1;
                else
                    cards[cards.Count - 1].Value = i;
            }

            ShuffleDeck();
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();
            int i = cards.Count;

            while (i > 1)
            {
                i--;
                int e = rand.Next(i + 1);
                Cards card = cards[e];
                cards[e] = cards[i];
                cards[i] = card;
            }
        }

        public Cards DealCard()
        {
            if (cards.Count <= 0)
            {
                this.Initialize();
            }

            Cards returnCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return returnCard;
        }

        public int RemaningCards()
        {
            return cards.Count;
        }

         
    }
}
