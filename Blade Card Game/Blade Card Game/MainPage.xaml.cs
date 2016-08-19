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
        private bool cardDrawn = false;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Reset()
        {
            var playerCards = new[] { _playerCard1, _playerCard2, _playerCard3, _playerCard4, _playerCard5, _playerCard6, _playerCard7, _playerCard8, _playerCard9, _playerCard10 };
            var aiCards = new[] { _aiCard1, _aiCard2, _aiCard3, _aiCard4, _aiCard5, _aiCard6, _aiCard7, _aiCard8, _aiCard9, _aiCard10 };
            var aiCardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
            var playerCardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };
            
            for (int i = 0; i < 10; i++)
            {
                aiCardArea[i].Source = null;
                aiCards[i].Source = null;
                aiCards[i].Visibility = Visibility.Visible;

                playerCardArea[i].Source = null;
                playerCards[i].Source = null;
                playerCards[i].Visibility = Visibility.Visible;
            }
            aiDrawedCard = new List<Cards>();
            playerDrawedCard = new List<Cards>();
            aiCardsInHand = new List<Cards>();
            cardsInHand = new List<Cards>();

            _txtAiScore.Text = "0";
            _txtPlayerScore.Text = "0";

            _playerDeckCard.Source = null;
            _aiDeckCard.Source = null;

            _btnStart.Visibility = Visibility.Visible;
            startButton = false;
            cardDrawn = false;
        }

        //Method for distributing the cards when the deck is clicked. 
        private async void _playerDeck_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (startButton == true && cardDrawn == false)
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
                        playerTurn = true;
                    }
                    cardDrawn = true;
                }
                else
                {
                    _playerDeck.Visibility = Visibility.Collapsed;
                    var message = new MessageDialog("There are no more cards! Game is a tie.");
                    await message.ShowAsync();
                    Reset();
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

        private void IsItBolt(int i, string player)
        {
            if (player == "ai")
            {
                if (aiCardsInHand[i].Face == Face.bolt)
                {
                    BoltCard(player);
                }
                else if (aiCardsInHand[i].Face == Face.mirror)
                {
                    MirrorCard();
                }
            }
            else if(player == "player")
            {
                if (cardsInHand[i].Face == Face.bolt)
                {
                    BoltCard(player);
                }
                else if (cardsInHand[i].Face == Face.mirror)
                {
                    MirrorCard();
                }
            }
        }

        private void MirrorCard()
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

        private void BoltCard(string player)
        {
            if(player == "player")
            {
                var cardArea = new[] { _aiPlayedCard1, _aiPlayedCard2, _aiPlayedCard3, _aiPlayedCard4, _aiPlayedCard5, _aiPlayedCard6, _aiPlayedCard7, _aiPlayedCard8, _aiPlayedCard9, _aiPlayedCard10 };
                for (int i = 9; i > -1; i--)
                {
                    if (cardArea[i].Source != null)
                    {
                        cardArea[i].Source = null;
                        break;
                    }
                    else if(i == 0)
                    {
                        _aiDeckCard.Source = null;
                    }
                }
                aiScore = Convert.ToInt32(_txtAiScore.Text) - _game.CardValue(aiCardImage);
                _txtAiScore.Text = Convert.ToString(aiScore);
            }
            else if(player == "ai")
            {
                var cardArea = new[] { _playerPlayedCard1, _playerPlayedCard2, _playerPlayedCard3, _playerPlayedCard4, _playerPlayedCard5, _playerPlayedCard6, _playerPlayedCard7, _playerPlayedCard8, _playerPlayedCard9, _playerPlayedCard10 };
                for (int i = 9; i > -1; i--)
                {
                    if (cardArea[i].Source != null)
                    {
                        cardArea[i].Source = null;
                        break;
                    }
                    else if (i == 0)
                    {
                        _playerDeckCard.Source = null;
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
            if (_aiCard1.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard1);
                _aiCard1.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[0].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(0, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard2.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard2);
                _aiCard2.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[1].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(1, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard3.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard3);
                _aiCard3.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[2].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(2, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard4.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard4);
                _aiCard4.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[3].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(3, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard5.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard5);
                _aiCard5.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[4].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(4, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard6.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard6);
                _aiCard6.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[5].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(5, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard1.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard7);
                _aiCard7.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[6].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(6, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard8.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard8);
                _aiCard8.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[7].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(7, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard9.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard9);
                _aiCard9.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[8].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(8, "ai");
                whoWon("ai");
            }
        }

        private void _aiCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_aiCard10.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == false)
            {
                aiEmptySlot(_aiCard10);
                _aiCard10.Visibility = Visibility.Collapsed;

                aiCardImage = $"ms-appx:///Assets/card {aiCardsInHand[9].Face}.gif";
                aiScore = Convert.ToInt32(_txtAiScore.Text) + _game.CardValue(aiCardImage);

                _txtAiScore.Text = Convert.ToString(aiScore);
                playerTurn = true;
                IsItBolt(9, "ai");
                whoWon("ai");
            }
        }

        private void _playerCard1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(_playerCard1.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard1);
                _playerCard1.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[0].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(0, "player");
                whoWon("player");
            }
        }

        private void _playerCard2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard2.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard2);
                _playerCard2.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[1].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(1, "player");
                whoWon("player");
            }
        }

        private void _playerCard3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard3.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard3);
                _playerCard3.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[2].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(2, "player");
                whoWon("player");
            }
        }
        private void _playerCard4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard4.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard4);
                _playerCard4.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[3].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(3, "player");
                whoWon("player");
            }
        }
        private void _playerCard5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard5.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard5);
                _playerCard5.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[4].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(4, "player");
                whoWon("player");
            }
        }

        private void _playerCard6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard6.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard6);
                _playerCard6.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[5].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(5, "player");
                whoWon("player");
            }
        }

        private void _playerCard7_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard7.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard7);
                _playerCard7.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[6].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(6, "player");
                whoWon("player");
            }
        }

        private void _playerCard8_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard8.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard8);
                _playerCard8.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[7].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(7, "player");
                whoWon("player");
            }
        }

        private void _playerCard9_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard9.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard9);
                _playerCard9.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[8].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(8, "player");
                whoWon("player");
            }
        }

        private void _playerCard10_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (_playerCard10.Visibility != Visibility.Collapsed && cardDrawn == true && playerTurn == true)
            {
                playeremptySlot(_playerCard10);
                _playerCard10.Visibility = Visibility.Collapsed;

                playerCardImage = $"ms-appx:///Assets/card {cardsInHand[9].Face}.gif";
                playerScore = Convert.ToInt32(_txtPlayerScore.Text) + _game.CardValue(playerCardImage);

                _txtPlayerScore.Text = Convert.ToString(playerScore);
                playerTurn = false;
                IsItBolt(9, "player");
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
                    var message = new MessageDialog("Player 1 has won game!!");
                    await message.ShowAsync();
                    Reset();
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
                                Reset();
                            }
                        }
                    }
                }
            }
            if (player == "player" && firstTurn == "player")
            {
                if (playerScore < aiScore)
                {
                    var message = new MessageDialog("Player 2 has won game!!");
                    await message.ShowAsync();
                    Reset();
                }
            }
            if (player == "ai" && firstTurn == "ai")
            {
                if (playerScore > aiScore)
                {
                    var message = new MessageDialog("Player 1 has won game!!");
                    await message.ShowAsync();
                    Reset();
                }
            }
            if(player == "player" && firstTurn == "ai")
            {
                if (aiScore > playerScore)
                {
                    var message = new MessageDialog("Player 2 has won game!!");
                    await message.ShowAsync();
                    Reset();
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
                                Reset();
                            }
                        }
                    }
                }
            }
            if (aiScore == playerScore && _playerDeck.Visibility == Visibility.Collapsed)
            {
                var message = new MessageDialog("Game is a tie!!");
                await message.ShowAsync();
                Reset();
            }
            if (aiScore == playerScore || playerScore == aiScore && _playerDeck.Visibility == Visibility.Visible)
            {
                var aiHand = new[] { _aiCard1, _aiCard2, _aiCard3, _aiCard4, _aiCard5, _aiCard6, _aiCard7, _aiCard8, _aiCard9, _aiCard10 };
                int j = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (aiHand[i].Visibility == Visibility.Collapsed)
                    {
                        j++;
                        if (j == 10)
                        {
                            var message = new MessageDialog("Player 1 has won game!!");
                            await message.ShowAsync();
                            Reset();
                        }
                    }
                }

                var hand = new[] { _playerCard1, _playerCard2, _playerCard3, _playerCard4, _playerCard5, _playerCard6, _playerCard7, _playerCard8, _playerCard9, _playerCard10 };
                int z = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (hand[i].Visibility == Visibility.Collapsed)
                    {
                        z++;
                        if (z == 10)
                        {
                            var message = new MessageDialog("Player 2 has won game!!");
                            await message.ShowAsync();
                            Reset();
                        }
                    }
                }
                if (j != 10 && z != 10)
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

                    cardDrawn = false;
                    _txtAiScore.Text = "0";
                    _txtPlayerScore.Text = "0";
                }
            }
        }

    }
}
