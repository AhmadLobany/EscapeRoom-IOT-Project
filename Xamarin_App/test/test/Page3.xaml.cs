using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Diagnostics;

namespace test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
 
        public Page3()
        {
            InitializeComponent();
        }

        async Task<String> AccessTheWebAsync(String url)
        {

            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync(url);
            string urlContents = await getStringTask;
            return urlContents;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var url = "https://getpuzzlesdata.azurewebsites.net/api/GetUsername?code=JkAx2Q8pLuDqWbOH7oCTZ9u8L1tTaXEW6ozdtmUhQpqWpA2tCeZAnw==";
            var User_name = "";
            Task task = new Task(() => {
                User_name = AccessTheWebAsync(url).Result;
            });
            task.Start();
            task.Wait();
            Debug.WriteLine(User_name);
            var url2 = "https://getpuzzlesdata.azurewebsites.net/api/GetPassword?code=wd36yPT59aYWhczUF8h3ZhiAxhgvqZ63hIkEnAsnZJOlRWN3dRav2w==";
            var pwd = "";
            Task task2 = new Task(() => {
                pwd = AccessTheWebAsync(url2).Result;
            });
            task2.Start();
            task2.Wait();
            Debug.WriteLine(pwd);


            if (Username.Text != User_name || Password.Text != pwd)
            {
                await DisplayAlert("Alert", "Invalid Username Or Password", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new Page4());
            }
        }
    }
}