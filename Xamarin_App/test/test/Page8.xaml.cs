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
    public partial class Page8 : ContentPage
    {
        public Page8()
        {
            InitializeComponent();
            Puz3.Source = ImageSource.FromResource("test.Puzzle3.png");
        }
        private async void Puz3_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Hint1", "Hanging the right object on the load cell will solve this puzzle.", "Ok");
        }
    }
}