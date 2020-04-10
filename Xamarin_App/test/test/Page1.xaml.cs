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
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            Img2.Source = ImageSource.FromResource("test.think.png");

        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3());
        }
    }
}