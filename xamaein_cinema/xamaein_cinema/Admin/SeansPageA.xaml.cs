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
    public partial class SeansPageA : ContentPage
    {
        public SeansPageA()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ShowFilms();
        }
        private void ShowFilms()
        {
            myListView.ItemsSource = App.database.GetSes();
        }

        private void myListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            session selectedFilms = (session)e.SelectedItem;
            this.Navigation.PushAsync(new SeansPageCU(selectedFilms));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SeansPageCU(null));
        }
    }
}