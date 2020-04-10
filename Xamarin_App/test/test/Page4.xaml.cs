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
    public partial class Page4 : ContentPage
    {
        public Page4()
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

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            var url = "https://updatepuzzles.azurewebsites.net/api/Puzzle1?code=CpmLpvYZjR7eSnCVCmpw9c21yX6mkPJucWgRdIgrb7rhGYVJFbUUwA==";
            var puz1 = "";
            Task task = new Task(() => {
                puz1= AccessTheWebAsync(url).Result;
            });
            task.Start();
            task.Wait();
            Debug.WriteLine(puz1);
            var url2 = "https://updatepuzzles.azurewebsites.net/api/Puzzle2?code=KHYqJvfA5y63xmBeQm6r9wykr4k0v3OqK1tR89GzPbahMEgVA8Hhdw==";
            var puz2 = "";
            Task task2 = new Task(() => {
                puz2 = AccessTheWebAsync(url2).Result;
            });
            task2.Start();
            task2.Wait();
            Debug.WriteLine(puz2);
            string puzzle2 = "";
            for(int i=0;i<12;i++)
            {
                if(puz2[i]=='1')
                {
                    puzzle2 = puzzle2 + "-Blue";
                }
                if (puz2[i] == '2')
                {
                    puzzle2 = puzzle2 + "-Red";
                }
                if (puz2[i] == '3')
                {
                    puzzle2 = puzzle2 + "-Green";
                }
            }
            await DisplayAlert("Done", "Updated Successfully!\n" + "Puzzle1: " + puz1 + "\n" + "Puzzle2: " + puzzle2, "Ok");
         
        }

        private async void Current_Clicked(object sender, EventArgs e)
        {
            var url = "https://getpuzzlesdata.azurewebsites.net/api/GetMessagePuz1?code=m5SjCtKU/NZ4hswamb9qw3P7MKXg0hxoIHFjeugAOHTmkLhE31TNlA==";
            var puz1 = "";
            Task task = new Task(() => {
                puz1 = AccessTheWebAsync(url).Result;
            });
            task.Start();
            task.Wait();
            Debug.WriteLine(puz1);
            var url2 = "https://getpuzzlesdata.azurewebsites.net/api/GetColors?code=nKXiqXphjrDUBd4d94jpT8JFEaUhMnJdqHpMH5ALxEY4e/QH7Lwqhg==";
            var puz2 = "";
            Task task2 = new Task(() => {
                puz2 = AccessTheWebAsync(url2).Result;
            });
            task2.Start();
            task2.Wait();
            Debug.WriteLine(puz2);
            string puzzle2 = "";
            for (int i = 0; i < 12; i++)
            {
                if (puz2[i] == '1')
                {
                    puzzle2 = puzzle2 + "-Blue";
                }
                if (puz2[i] == '2')
                {
                    puzzle2 = puzzle2 + "-Red";
                }
                if (puz2[i] == '3')
                {
                    puzzle2 = puzzle2 + "-Green";
                }
            }
            await DisplayAlert("Cyphers", "Puzzle1: " + puz1 + "\n" + "Puzzle2: " + puzzle2, "Ok");
        }
    }
}