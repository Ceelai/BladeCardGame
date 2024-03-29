﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

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


            _playerValue = 0;

            _dealerValue = 0;

            _playerScore = 0;

            _dealerScore = 0;

        }

        //Deal cards method so that when either house or player needs more cards. The method will give more cards from the deck 
        public List<Cards> DealCards(List<Cards> cardsInHand)
        {
            Deck hand = new Deck();
            //List<Cards> cardsInHand = new List<Cards>();
            int i = 0;

            while (i < 10)
            {
                cardsInHand.Add(hand.DealHand());
                i++;
            }

            return cardsInHand;
        }

        //This will identify the card value based on which image is pulled from the deck. 
        public int CardValue(string cardValue)
        {
            int value = 0;

            switch (cardValue)
            {
                case "ms-appx:///Assets/card one.gif":
                case "ms-appx:///Assets/card bolt.gif":
                case "ms-appx:///Assets/card mirror.gif":
                    value = 1;
                    return value;

                case "ms-appx:///Assets/card two.gif":
                    value = 2;
                    return value;

                case "ms-appx:///Assets/card three.gif":
                    value = 3;
                    return value;

                case "ms-appx:///Assets/card four.gif":
                    value = 4;
                    return value;

                case "ms-appx:///Assets/card five.gif":
                    value = 5;
                    return value;

                case "ms-appx:///Assets/card six.gif":
                    value = 6;
                    return value;

                case "ms-appx:///Assets/card seven.gif":
                    value = 7;
                    return value;

                case "ms-appx:///Assets/card eight.gif":
                    value = 8;
                    return value;

                case "ms-appx:///Assets/card nine.gif":
                    value = 9;
                    return value;

                case "ms-appx:///Assets/card ten.gif":
                    value = 10;
                    return value;
            }
            return value;
        }

        //
        public void SaveData()
        {

            string line;
            //Opens the txt file, reads the number inside and replaces it with the new score. 
            try

            {
                FileStream aFile = new FileStream("SaveFile.txt", FileMode.OpenOrCreate);


                //this piece of snickerdoodle reads to goddamn text file 
                StreamReader sr = new StreamReader(aFile);



                //this reads the lines found inside the .txt file 
                line = sr.ReadLine();

                try
                {


                    StreamWriter write = new StreamWriter(aFile);




                    string newscore = "1";
                    //if the file has not been created yet, add the initial 1 to it 
                    if (line == "")
                    {

                        write.WriteLine($"{newscore}");

                    }
                    //if the file has been created, add the score thats already there plus 1 
                    else
                    {
                        line += newscore;
                    }

                }


                catch (IOException e)
                {
                    var message = new MessageDialog("BRO U SCORE DUN GOOFED, hehe xD");
                    return;
                }






            }

            catch (IOException e)
            {
                var message = new MessageDialog("Call 1866 WOW DEAL, and GETAWAY FOR LESS");
                return;
            }
        }

    }
}
