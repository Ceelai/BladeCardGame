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

        private Game _game = new Game();
        private Deck deck = new Deck();

        private List<Cards> playerDrawedCard = new List<Cards>();
        private List<Cards> aiDrawedCard = new List<Cards>();
        private List<Cards> cardsInHand = new List<Cards>();
        private List<Cards> aiCardsInHand = new List<Cards>();

        private string playerCardImage;
        private string aiCardImage;
        private string firstTurn;
        
        private int playerScore = 0;
        private int aiScore = 0;

        private bool startButton = false;
        private bool playerTurn = true;


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

                    aiScore = Convert.ToInt32(_txtAiScore.Text);
                    playerScore = Convert.ToInt32(_txtPlayerScore.Text);

                    if (aiScore < playerScore)
                    {
                        playerTurn = false;
                        firstTurn = "ai";
                    }
                    else
                    {
                        firstTurn = "player";
                    }
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
        }

        private void isItBolt(int i, string player)
        {
            if (player == "ai")
            {
                if (aiCardsInHand[i].Face == Face.bolt)
                {
                    boltCard(player);
                }
                else if (aiCardsInHand[i].Face == Face.mirror)
                {
                    mirrorCard();
                }
            }
            else if(player == "player")
            {
                if (cardsInHand[i].Face == Face.bolt)
                {
                    boltCard(player);
                }
                else if (cardsInHand[i].Face == Face.mirror)
                {
                    mirrorCard();
                }
            }
        }

        private void mirrorCard()
        {
            var aiCardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
            var playerCardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };

            for (int i = 0; i < 10; i++)
            {
                var tempCard = aiCardArea[i].Source;
                aiCardArea[i].Source = playerCardArea[i].Source;
                playerCardArea[i].Source = tempCard;
            }
            string tempScore = _txtAiScore.Text;
            _txtAiScore.Text = _txtPlayerScore.Text;
            _txtPlayerScore.Text = tempScore;
        }

        private void boltCard(string player)
        {
            if(player == "player")
            {
                var cardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
                for (int i = 9; i > 0; i--)
                {
                    if (cardArea[i].Source != null)
                    {
                        cardArea[i].Source = null;
                        break;
                    }
                }
                aiScore = Convert.ToInt32(_txtAiScore.Text) - _game.CardValue(aiCardImage);
                _txtAiScore.Text = Convert.ToString(aiScore);
            }
            else if(player == "ai")
            {
                var cardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };
                for (int i = 9; i > 0; i--)
                {
                    if (cardArea[i].Source != null)
                    {
                        cardArea[i].Source = null;
                        break;
                    }
                }
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) - _game.CardValue(playerCardImage);
                _txtPlayerScore.Text = Convert.ToString(playerScore);
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
            if (_aiCard1.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard1);
                _aiCard1.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(0, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard2.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard2);
                _aiCard2.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[1].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(1, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard3.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard3);
                _aiCard3.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[2].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(2, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard4.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard4);
                _aiCard4.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[3].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(3, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard5.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard5);
                _aiCard5.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[4].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(4, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard6.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard6);
                _aiCard6.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[5].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(5, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard1.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard7);
                _aiCard7.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[6].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(6, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard8.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard8);
                _aiCard8.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[7].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(7, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard9.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard9);
                _aiCard9.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[8].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(8, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard10.Visibility != Visibility.Collapsed && _aiDeckCard.Source != null && playerTurn == false)
            {
                aiEmptySlot(_aiCard10);
                _aiCard10.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[9].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                isItBolt(9, "ai");
                whoWon("ai");
            }
        }

        private void _playerCard1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(_playerCard1.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard1);
                _playerCard1.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[0].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(0, "player");
                whoWon("player");
            }
        }

        private void _playerCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard2.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard2);
                _playerCard2.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[1].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(1, "player");
                whoWon("player");
            }
        }

        private void _playerCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard3.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard3);
                _playerCard3.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[2].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(2, "player");
                whoWon("player");
            }
        }
        private void _playerCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard4.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard4);
                _playerCard4.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[3].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(3, "player");
                whoWon("player");
            }
        }
        private void _playerCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard5.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard5);
                _playerCard5.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[4].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(4, "player");
                whoWon("player");
            }
        }

        private void _playerCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard6.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard6);
                _playerCard6.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[5].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(5, "player");
                whoWon("player");
            }
        }

        private void _playerCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard7.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard7);
                _playerCard7.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[6].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(6, "player");
                whoWon("player");
            }
        }

        private void _playerCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard8.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard8);
                _playerCard8.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[7].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(7, "player");
                whoWon("player");
            }
        }

        private void _playerCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard9.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard9);
                _playerCard9.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[8].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(8, "player");
                whoWon("player");
            }
        }

        private void _playerCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard10.Visibility != Visibility.Collapsed && _playerDeckCard.Source != null && playerTurn == true)
            {
                playeremptySlot(_playerCard10);
                _playerCard10.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[9].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                isItBolt(9, "player");
                whoWon("player");
            }
        }

        private async void whoWon(string player)
        {
            aiScore = Convert.ToInt32(_txtAiScore.Text);
            playerScore = Convert.ToInt32(_txtPlayerScore.Text);

            if(player == "ai" && firstTurn == "player")
            {
                if (aiScore < playerScore)
                {
                    startButton = false;
                    var message = new MessageDialog("Player 1 has won game!!");
                    await message.ShowAsync();
                } 
                else if(playerScore < aiScore)
                {
                    var hand = new[] { _playerCard1, _playerCard2, _playerCard3, _playerCard4, _playerCard5, _playerCard6, _playerCard7, _playerCard8, _playerCard9, _playerCard10 };
                    int j = 0;
                    for(int i = 0; i < 10; i++)
                    {
                        if (hand[i].Visibility == Visibility.Collapsed)
                        {
                            j++;
                            if(j == 10)
                            {
                                var message = new MessageDialog("Player 2 has won game!!");
                                await message.ShowAsync();
                            }
                        }
                    }
                }
            }
            else if(player == "player" && firstTurn == "ai")
            {
                if (aiScore > playerScore)
                {
                    startButton = false;
                    var message = new MessageDialog("Player 2 has won game!!");
                    await message.ShowAsync();
                }
                else if (playerScore > aiScore)
                {
                    var hand = new[] { _aiCard1, _aiCard2, _aiCard3, _aiCard4, _aiCard5, _aiCard6, _aiCard7, _aiCard8, _aiCard9, _aiCard10 };
                    int j = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        if (hand[i].Visibility == Visibility.Collapsed)
                        {
                            j++;
                            if (j == 10)
                            {
                                var message = new MessageDialog("Player 1 has won game!!");
                                await message.ShowAsync();
                            }
                        }
                    }
                }
            }
            else if (aiScore == playerScore && _playerDeck.Visibility == Visibility.Collapsed)
            {
                startButton = false;
                var message = new MessageDialog("Game is a tie!!");
                await message.ShowAsync();
            }
            else if (aiScore == playerScore && _playerDeck.Visibility != Visibility.Collapsed)
            {
                var cardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
                var playerCardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };

                _playerDeckCard.Source = null;
                _aiDeckCard.Source = null;

                for (int i = 0; i < 10; i++)
                {
                    cardArea[i].Source = null;
                    playerCardArea[i].Source = null;
                }
                var message = new MessageDialog("Score is a tie! Draw card from deck.");
                await message.ShowAsync();

                _txtAiScore.Text = "0";
                _txtPlayerScore.Text = "0";
            }
        }

    }
}
