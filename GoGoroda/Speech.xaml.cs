using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Yandex.SpeechKit;

namespace GoGoroda
{
    public partial class Speech : PhoneApplicationPage
    {
        public Speech()
        {
            InitializeComponent();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SpeechKitInitializer.InitializeAsync();
            SpeechCtrl.StartRecognition("ru-RU", LanguageModel.Maps);
        }

        private void SpeechCtrl_Finished(object sender, Yandex.SpeechKit.UI.RecognitionFinishedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml?w=" + e.Result, UriKind.Relative));
        }
    }
}