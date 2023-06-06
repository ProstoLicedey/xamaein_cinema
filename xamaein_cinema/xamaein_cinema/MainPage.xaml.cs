using Microsoft.Data.SqlClient;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xamaein_cinema.Data;
using xamaein_cinema.Models;
using Xamarin.Forms;

namespace xamaein_cinema
{
    public partial class MainPage : ContentPage
    {
        public List<films> FilmsList;
        public ObservableCollection<films> Films { get; set; }
        public films SelectedFilms { get; set; }
        public MainPage()
        {
            InitializeComponent();
           


            
        }
        protected override void OnAppearing()
        {
            Films = GetTickets();
            if (ThisUser.UserId != 0)
            {
                NameLabel.Text = App.Database.GetUsers(ThisUser.UserId).name;
            }

            this.BindingContext = this;

            Random random = new Random();
            int rnd = random.Next(2, 6);
            for (int j = 0; j < rnd; j++)
            {

                Button btn = new Button()
                {
                    TextColor = Color.White,
                    Text = "12:00",
                    WidthRequest = 70,
                    HeightRequest = 40,
                    BackgroundColor = Color.FromHex("#7700FF"),
                    Margin = new Thickness(5, 5, 5, 5),

                };
                btn.CornerRadius = 20;
            }
        }

        private  ObservableCollection<films> GetTickets()
        {
            FilmsList  = App.Database.GetFilms();
            return  new ObservableCollection<films>(FilmsList);
        }
        private void FilmsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                this.Navigation.PushAsync(new Seans_Page(SelectedFilms));
            }
        }
        private  async void Town_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Извините", "Данный сервис пока работает только в данном городе", "OK");
        }
        private async void Auth_Clicked(object sender, EventArgs e)
        {
            if (ThisUser.UserId == 0)
            {
                this.Navigation.PushAsync(new Auth_Page(false));
            }
            else if (ThisUser.AdminCheck){
                this.Navigation.PushAsync(new AdminTickets());
            }
            else
            {
                this.Navigation.PushAsync(new UserTickets());
            }
           
        }
    
    }

}
