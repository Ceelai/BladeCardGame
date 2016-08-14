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
        private Deck deck = new Deck();
        private static readonly BitmapImage s_cardBackImage;
        private List<Cards> drawedCard = new List<Cards>();

        private bool startButton = false;
        static MainPage()
        {
            
            s_cardBackImage = new BitmapImage(new Uri("ms-appx:///Assets/card back.gif"));
        }
        public MainPage()
        {
            this.InitializeComponent();
            
        }


        private async void _playerDeck_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (startButton == true && _playerPlayedCard1.Source == null)
            {
                if (drawedCard.Count < 12)
                {
                    drawedCard.Add(deck.DealCard());
                    _playerPlayedCard1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/card {drawedCard[drawedCard.Count - 1].Face}.gif"));
                }
                else
                {
                    _playerDeck.Visibility = Visibility.Collapsed;
                    var message = new MessageDialog("There are no more cards!");
                    await message.ShowAsync();
                }
            }
        }

        private void _btnStart_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _btnStart.Visibility = Visibility.Collapsed;
            startButton = true;
        }
    }
}
