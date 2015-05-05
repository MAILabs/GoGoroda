using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GoGoroda.Resources;
using Yandex.SpeechKit;

namespace GoGoroda
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            SpeechKitInitializer.Configure("7385ba8e-b595-4411-9404-093bff3e042c");
            SpeechKitInitializer.SetParameter("soundformat", "speex");
            protocol.ItemsSource = Game.Protocol;
        }

                protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.ContainsKey("w"))
            {
                var w = NavigationContext.QueryString["w"];
                if (Game.IsCity(w) && Game.Check(w))
                {
                    Game.Protocol.Add(w);
                    var r = Game.PickLetter(Game.Last(w));
                    Game.Protocol.Add(r);
                    protocol.ItemsSource = Game.Protocol;
                    await Game.Say(string.Format("Вы сказали {0}. Мой ответ: {1}.", w, r));
                }
                else
                {
                    var reason = Game.IsCity(w) ? "Буквы не совпадают" : "Это не город"; 
                    await Game.Say(string.Format("Вы сказали {1} и ошиблись. {2}. Вам надо назвать город на букву {0}.", Game.LastLetter,w,reason));
                }
            }
            else
            {
                var c = Game.PickCity();
                Game.Protocol.Add(c);
                protocol.ItemsSource = Game.Protocol;                
                await Game.Say(String.Format("Давай поиграем в города. Я начинаю. Мой город: {0}", c));
            }
            NavigationService.Navigate(new Uri("/Speech.xaml", UriKind.Relative));
        }

    }
}