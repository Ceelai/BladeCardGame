using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using BladeCardGameLogic;
using System.Collections.Generic;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Blade_Card_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Score score = new Score();
        private Game _game = new Game();
        private Deck deck = new Deck();
        private static readonly BitmapImage s_cardBackImage;
        private List<Cards> playerDrawedCard = new List<Cards>();
        private List<Cards> aiDrawedCard = new List<Cards>();
        private int playerScore = 0;
        private int aiScore = 0;

        private bool startButton = false;
        static MainPage()
        {

            s_cardBackImage = new BitmapImage(new Uri("ms-appx:///Assets/card back.gif"));
        }
        public MainPage()
        {
            this.InitializeComponent();




        }

        //Method for distributing the cards when the deck is clicked. 
        private async void _playerDeck_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (startButton == true && _playerPlayedCard1.Source == null)
            {
                if (playerDrawedCard.Count < 12)
                {
                    aiDrawedCard.Add(deck.DealCard());
                    playerDrawedCard.Add(deck.DealCard());
                    _playerPlayedCard1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/card {playerDrawedCard[playerDrawedCard.Count - 1].Face}.gif"));
                    _aiPlayedCard1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/card {aiDrawedCard[aiDrawedCard.Count - 1].Face}.gif"));
                }
                else
                {
                    _playerDeck.Visibility = Visibility.Collapsed;
                    var message = new MessageDialog("There are no more cards! Game is a tie.");
                    await message.ShowAsync();
                    startButton = false;
                }
            }



        }

        //Method for starting the game
        private void _btnStart_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _btnStart.Visibility = Visibility.Collapsed;
            startButton = true;

            //ask the game to play a round 
            _game.PlayRound();



            // figures out if the player card is either greater or small than the card and adds the score to the 
            //player respectively 
            if (_game._playerValue > _game._dealerValue)
            {
                score.PlayerScore();
                //converts textblock into the players score value in string format 
                _txtPlayerScore.Text = score.PlayerScore().ToString();
            }

            //same thing as above but for the dealer 
            if (_game._playerValue < _game._dealerValue)
            {
                score.DealerScore();
                _txtAiScore.Text = score.DealerScore().ToString();
            }

            
            //just in case something blows up, we have this message to save the day xD
            else
            {
                var message = new MessageDialog("Something went wrong :(. Contact game dev for this issue.");
            }
        }


    }
}
