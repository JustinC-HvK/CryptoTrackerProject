using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CoinAPI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft;
using Crypto.Model;

namespace Crypto
{
    public partial class MainPage : ContentPage
    {
        private string Key = "4D8CB57E-F91E-4CB6-82ED-8F4197A62214";
        private string imageURL = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";
        public MainPage()
        {
            InitializeComponent();
            coinView.ItemsSource = GetCoin();
        }
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            coinView.ItemsSource = GetCoin();
        }
        private List<Coin> GetCoin()
        {
            List<Coin> coins;
            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=ETH;XRP;BTC;LTC;DOGE");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", Key);
            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);
            foreach (var c in coins)
            {
                c.icon_url = imageURL + c.id_icon.Replace("-", "") + ".png";
            }
            return coins;
        }
    }
}
