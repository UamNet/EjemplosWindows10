using System;
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

using Windows.Web.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RestClientCSharp
{

    [DataContract]
    public class Quote
    {
        [DataMember]
        public int Id;
        [DataMember]
        public string Text;
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public ObservableCollection<Quote> quotes = new ObservableCollection<Quote>();

        public MainPage()
        {
            this.InitializeComponent();
            refreshQuotes();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //https://blogs.windows.com/buildingapps/2015/11/23/demystifying-httpclient-apis-in-the-universal-windows-platform/
            string newQuoteText = newQuote.Text;

            var uri = new Uri($"http://localhost:1337/api/quotes/new/{newQuoteText}");

            var httpClient = new HttpClient();

            try
            {
                var result = await httpClient.PostAsync(uri, new HttpStringContent(""));

                if (result.StatusCode == HttpStatusCode.Ok)
                {
                    messageBlock.Text = "Cita añadida con éxito";
                    refreshQuotes();
                }
                else
                {
                    messageBlock.Text = "Se ha producido un error en el servidor";
                }
            }
            catch
            {
                messageBlock.Text = "No se puede comunicar con el servidor";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            refreshQuotes();
        }

        private async void refreshQuotes()
        {
            string newQuoteText = newQuote.Text;

            var uri = new Uri($"http://localhost:1337/api/quotes/");

            var httpClient = new HttpClient();

            try
            {
                var result = await httpClient.GetAsync(uri);

                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Quote[]));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result.Content.ToString()));
                Quote[] quoteArray = (Quote[])js.ReadObject(stream);
                quotes.Clear();
                foreach (Quote q in quoteArray)
                {
                    quotes.Add(q);
                }

            }
            catch
            {
                messageBlock.Text = "No se puede comunicar con el servidor";
            }
        }
    }
}