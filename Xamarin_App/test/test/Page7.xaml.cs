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
    public partial class Page7 : ContentPage
    {
        public Page7()
        {
            InitializeComponent();
            Puz2.Source = ImageSource.FromResource("test.Puzzle2.png");
        }
        private async void Puz2_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Hint1", "Showing the correct RGB colors subsequence(the length is 12) will solve this puzzle.\n" +
                 "We require just a correct 'subsequence' of the colors", "Ok");
        }
    }
}