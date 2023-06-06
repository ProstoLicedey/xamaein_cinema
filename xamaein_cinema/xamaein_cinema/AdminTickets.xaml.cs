using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTickets : ContentPage
    {
        tickets Tick;
        public AdminTickets()
        {
            InitializeComponent();
        }
        private async void Poisk_btn(object sender, System.EventArgs e)
        {
            try
            {
                
                 Tick = App.Database.GeteTickets(Convert.ToInt32(TicketEntry.Text));
                users User = App.Database.GetUsers(Convert.ToInt32(Tick.user_id));
                session Ses = App.Database.GetSes(Convert.ToInt32(Tick.session_id));
                films Film = App.Database.GetFilms(Convert.ToInt32(Ses.film_id)); 
                Dateinfo.Text = "Дата и время: " + Ses.date_time;
                FilmInfo.Text = "Фильм: " + Film.name;
                Zalinfo.Text = "Зал: " + Ses.number_zal.ToString();
                Readinfo.Text = "Ряд: " + Tick.row_number.ToString();
                Mestoinfo.Text = "Место: " + Tick.seat_number.ToString();
                Surnamrinfo.Text = "фамилия: " + User.surname;
                Information.IsVisible = true;
                prohod.IsEnabled = true;
                if (Tick.check_control)
                {
                    await DisplayAlert("Внимание", "Билет уже использован", "OK");
                    prohod.IsEnabled = false;
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Билет не найден ", "OK");
                Information.IsVisible = false;
                TicketEntry.Text = "";
            }
        
        }

        private async void Sbros_btn(object sender, System.EventArgs e)
        {
            Information.IsVisible = false;
            TicketEntry.Text = "";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Tick.check_control = true;
            App.Database.UpdateTickets(Tick);
            Information.IsVisible = false;
            TicketEntry.Text = "";
        }
    }
}