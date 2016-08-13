using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    /// <summary>
    /// Game class where most of the card handling will take place. 
    /// </summary>
    public class Game
    {
        //field variable for the playerscore
        public int _playerScore;


        //field variable for the dealerscore/housescore 
        public int _dealerScore ;


        //field variable for the players card value 
        public int _playerValue;

        //field variable for the house card value 
        public int _dealerValue;



        //constructor for the Game Class
        public Game()
        {
            Game _game = new Game();

            _playerValue = 0;

            _dealerValue = 0;

            _playerScore = 0;

            _dealerScore = 0;

        }

        //Deal cards method so that when either house or player needs more cards. The method will give more cards from the deck 
        public void DealCards()
        {

        }

        //The "Start" method of the game. Will run when the users are ready to play. 
        public void PlayRound()
        {

            //compare the player card with the house card 



        }

        //This will identify the card value based on which image is pulled from the deck. 
        public void CardValue()
        {

        }

    }
}
