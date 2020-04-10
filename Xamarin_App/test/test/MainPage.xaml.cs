using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
namespace test
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            logo.Source = ImageSource.FromResource("test.logo_transparent_background.png");
        }
        async Task<String> AccessTheWebAsync(String url)
        {
           
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync(url);
            string urlContents = await getStringTask;
            return urlContents;
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var url = "https://getpuzzlesdata.azurewebsites.net/api/GetMessagePuz1?code=m5SjCtKU/NZ4hswamb9qw3P7MKXg0hxoIHFjeugAOHTmkLhE31TNlA==";
            var myXMLstring = "";
            Task task = new Task(() => {
                myXMLstring = AccessTheWebAsync(url).Result;
            });
            task.Start();
            task.Wait();
            Debug.WriteLine(myXMLstring);

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = myXMLstring }
                }
            };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

    }





}

