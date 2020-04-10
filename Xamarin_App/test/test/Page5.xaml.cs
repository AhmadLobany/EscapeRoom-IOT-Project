using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page5 : ContentPage
    {
        int min = 0;
        int sec = 0;
        int sec1 = 0;
        public Page5()
        {
            InitializeComponent();
            logo.Source = ImageSource.FromResource("test.logo_transparent_background.png");
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                sec++;
                min = sec / 60;
                sec1 = sec % 60;
                Timer.Text = min + ":" + sec1;
                if(min>=15)
                {
                    DisplayAlert("Game Over", "Time is over!", "Exit Game");
                    return false;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        async Task<String> AccessTheWebAsync(String url)
        {

            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync(url);
            string urlContents = await getStringTask;
            return urlContents;
        }


        private async void Done_Clicked(object sender, EventArgs e)
        {
            var url = "https://getpuzzlesdata.azurewebsites.net/api/GetStr1?code=yh2xMI3DVGXoQ5P3ewe0ytRdnF7c6kMRFY7udM41aBfBg9GabZ6fVw==";
            var str1 = "";
            Task task = new Task(() => {
                str1 = AccessTheWebAsync(url).Result;
            });
            task.Start();
            task.Wait();
            Debug.WriteLine(str1);
            var url2 = "https://getpuzzlesdata.azurewebsites.net/api/GetStr2?code=YYsyri9Bqfw3OiZuLdFJwuXMdoc4a29vVz4COEHs6JJUoYCXSvwhzA==";
            var str2= "";
            Task task2 = new Task(() => {
                str2 = AccessTheWebAsync(url2).Result;
            });
            task2.Start();
            task2.Wait();
            Debug.WriteLine(str2);
            var url3 = "https://getpuzzlesdata.azurewebsites.net/api/GetStr3?code=6L8X/0qFnXfVhAoVIfCVXo8jytxm7wu2dx0eRBwVjanGBXNinEMB9Q==";
            var str3 = "";
            Task task3 = new Task(() => {
                str3 = AccessTheWebAsync(url3).Result;
            });
            task3.Start();
            task3.Wait();
            Debug.WriteLine(str3);

            if (Puz1.Text != str1 || Puz2.Text != str2 || Puz3.Text != str3)
            {
                await DisplayAlert("Alert", "Invalid Strings,TRY AGAIN!!","Ok");
            }
            else
            {
                await DisplayAlert("Awesome", "Congratulations! Wooow!" + "\n" + "You solved the game in " + min + " Minutes," + sec1 + " Seconds."  , "Ok");
                await Navigation.PushAsync(new Page9());
            }
        }

        private async void Puz1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page6());
        }
        private async void Puz2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page7());
        }
        private async void Puz3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page8());
        }
    }
}