using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamaein_cinema.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new FilmPageA());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SeansPageA());
        }
    }
}