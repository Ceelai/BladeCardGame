using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using BladeCardGameLogic;
using System.Collections.Generic;
using Windows.UI.Popups;
using System.Threading;

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
        private List<Cards> cardsInHand = new List<Cards>();
        private List<Cards> aiCardsInHand = new List<Cards>();
        private string playerCardImage;
        private string aiCardImage;
        
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
            if (startButton == true && _playerDeckCard.Source == null)
            {
                if (playerDrawedCard.Count < 12)
                {
                    aiDrawedCard.Add(deck.DealCard());
                    playerDrawedCard.Add(deck.DealCard());

                    playerCardImage = $"ms-appx:///Assets/card {playerDrawedCard[playerDrawedCard.Count - 1].Face}.gif";
                    aiCardImage = $"ms-appx:///Assets/card {aiDrawedCard[aiDrawedCard.Count - 1].Face}.gif";
                    _playerDeckCard.Source = new BitmapImage(new Uri(playerCardImage));
                    _aiDeckCard.Source = new BitmapImage(new Uri(aiCardImage));

                    _txtPlayerScore.Text = Convert.ToString(_game.CardValue(playerCardImage));
                    _txtAiScore.Text = Convert.ToString(_game.CardValue(aiCardImage));
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
            int i = 0;
            
            string card = "";
            string aiCard = "";
            var playerCards = new[] { _playerCard1, _playerCard2, _playerCard3, _playerCard4, _playerCard5, _playerCard6, _playerCard7, _playerCard8, _playerCard9, _playerCard10};
            var aiCards = new[] { _aiCard1, _aiCard2, _aiCard3, _aiCard4, _aiCard5, _aiCard6, _aiCard7, _aiCard8, _aiCard9, _aiCard10};
            //ask the game to play a round 
            _game.RoundWinner();
            
            if (startButton == true)
            {
                _game.DealCards(cardsInHand);
                _game.DealCards(aiCardsInHand);
                while (i != 10)
                {
                    card = $"ms-appx:///Assets/card {cardsInHand[i].Face}.gif";
                    aiCard = $"ms-appx:///Assets/card {aiCardsInHand[i].Face}.gif";
                    aiCards[i].Source = new BitmapImage(new Uri(aiCard));
                    playerCards[i].Source = new BitmapImage(new Uri(card));
                    i++;

                }
            }
            

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

        private void aiEmptySlot(Image playedCard)
        {
            var cardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
            for (int i = 0; i < 10; i++)
            {
                if (cardArea[i].Source == null)
                {
                    cardArea[i].Source = playedCard.Source;
                    break;
                }
            }
        }
        private void playeremptySlot(Image playedCard)
        {
            var cardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };
            for (int i = 0; i < 10; i++)
            {
                if (cardArea[i].Source == null)
                {
                    cardArea[i].Source = playedCard.Source;
                    break;
                }
            }
        }
        private void _aiCard1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard1.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard1);
                _aiCard1.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard2.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard2);
                _aiCard2.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard3.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard3);
                _aiCard3.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard4.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard4);
                _aiCard4.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard5.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard5);
                _aiCard5.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard6.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard6);
                _aiCard6.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard1.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard7);
                _aiCard7.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard8.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard8);
                _aiCard8.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard9.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard9);
                _aiCard9.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _aiCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard10.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null)
            {
                aiEmptySlot(_aiCard10);
                _aiCard10.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
            }
        }

        private void _playerCard1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(_playerCard1.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard1);
                _playerCard1.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[0].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard2.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard2);
                _playerCard2.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[1].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard3.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard3);
                _playerCard3.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[2].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }
        private void _playerCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard4.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard4);
                _playerCard4.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[3].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }
        private void _playerCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard5.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard5);
                _playerCard5.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[4].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard6.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard6);
                _playerCard6.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[5].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard7.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard7);
                _playerCard7.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[6].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard8.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard8);
                _playerCard8.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[7].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard9.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard9);
                _playerCard9.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[8].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

        private void _playerCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard10.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null)
            {
                playeremptySlot(_playerCard10);
                _playerCard10.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[9].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
            }
        }

    }
}
