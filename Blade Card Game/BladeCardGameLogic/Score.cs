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

            if (_game._playerValue > _game._dealerScore)
            {
                _game._playerScore = _game._playerScore++;
            }

            return _game._playerScore;
        }

        //DealerScore method when the dealer wins 
        public int DealerScore()
        {
            if (_game._houseValue > _game._playerScore)
            {
                _game._dealerScore = _game._dealerScore++;
            }
            return _game._dealerScore;
        }


    }
}
