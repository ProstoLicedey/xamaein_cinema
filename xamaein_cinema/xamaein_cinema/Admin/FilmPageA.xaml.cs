using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamaein_cinema.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmPageA : ContentPage
    {
        public FilmPageA()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ShowFilms();
        }
        private void ShowFilms()
        {
            myListView.ItemsSource = App.database.GetFilms();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            films selectedFilms = (films)e.SelectedItem;
            this.Navigation.PushAsync(new FilmPageCU(selectedFilms));

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            this.Navigation.PushAsync(new FilmPageCU(null));
        }
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}