using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page6 : ContentPage
    {
        public Page6()
        {
            InitializeComponent();
            Puz1.Source = ImageSource.FromResource("test.Puzzle1.png");
        }
        private async void Puz1_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Hint1", "Part 1: Figure the string which is shown by letters in  morse code machine using two LEDS (one indecates '.' and the other '_')\n"+ 
                "Part 2: Decode the cypher string using the morse code poster " +
                "Part 3: Click איק button and insert the numbers by holding any object in front of the sensor", "Ok");
        }

        private async void Puz1_Clicked2(object sender, EventArgs e)
        {
            await DisplayAlert("Hint2", "The string is a sequence of 5 numbers seperated with 'S' Letter", "OK");
        }
    }
}