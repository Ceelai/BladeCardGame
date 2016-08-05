﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BladeCardGameLogic;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Blade_Card_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        //represents the link between the presentation tier and logic tier 
        private BladeCardGameLogic.CardDeck _game;


        private static readonly BitmapImage s_cardBackImage;

        static MainPage()
        {
            s_cardBackImage = new BitmapImage(new Uri("ms-appx:///Assets/word-ace-card-back.jpg"));
        }
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnStartGame(object sender, RoutedEventArgs e)
        {

        }
    }
}
