using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    /// <summary>
    /// Score class where the counting of points will be kept. 
    /// </summary>
    public class Score
    {
        //create instance of the Game Class 
        Game _game = new Game();

        //PlayerScore method when the player wins 
        public int PlayerScore()
        {

            //Value of the player's winning card is then added to the players current score.            
            _game._playerScore = _game._playerScore + _game._playerValue;

            
            //returns the new players score 
            return _game._playerScore;

        }

        //DealerScore method when the dealer wins 
        public int DealerScore()
        {
            _game._dealerScore = _game._dealerScore + _game._dealerValue;

            return _game._dealerValue;
        }


    }
}
