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
        Game game = new Game();

        //PlayerScore method when the player wins 
        public int PlayerScore()
        {
            return game._playerScore + 1;
        }

        //DealerScore method when the dealer wins 
        public int DealerScore()
        {
            return game._dealerScore + 1;
        }


    }
}
