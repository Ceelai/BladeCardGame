using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    class ValueCards
    {

        //field variable for the card value 
        private byte _value;


        //field variable for the max card value 
        public const int MAX_CARD_VALUE = 13;


        /// <summary>
        /// Constructor that needs value to create a card. 
        /// </summary>
        /// <param name="value"></param>
        public ValueCards(byte value)
        {
            _value = value;
        }

        //Properties 


        //Property for the value 
        public byte Value
        {
            get { return _value; }
            set { _value = value; }
        }


        /// <summary>
        /// The name of the card returns a string based on the _value field variable:
        ///    - Ace for 1
        ///    - King for 13
        ///    - Queen for 12
        ///    - Jack for 11
        ///    - the card value as a string for the rest of the cards
        /// </summary>
        public string Name
        {
            get
            {
                string cardName;

                switch (_value)
                {
                    case 1:
                        cardName = "Ace";
                        break;

                    case 13:
                        cardName = "King";
                        break;

                    case 12:
                        cardName = "Queen";
                        break;

                    case 11:
                        cardName = "Jack";
                        break;

                    default:
                        cardName = _value.ToString();
                        break;
                }

                return cardName;
            }
        }
    }
}
