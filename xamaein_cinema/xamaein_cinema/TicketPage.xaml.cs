using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketPage : ContentPage
    {
      
        public TicketPage(int TicketId)
        {
            InitializeComponent();
           
            tickets ThisTicket = App.Database.GeteTickets(TicketId);
            session ThisSes = App.Database.GetSes(ThisTicket.session_id);
            films ThisFilm = App.Database.GetFilms(ThisSes.film_id);
           
            NumberLabel.Text = "№" + ThisTicket.tickets_id.ToString();
            NameFilmLabel.Text = ThisFilm.name;
            DateLabel.Text = ThisSes.date_time;
            PriceLabel.Text =  ThisSes.price.ToString() + "₽";
            HallLabel.Text = ThisSes.number_zal.ToString();
            RowLabel.Text = ThisTicket.row_number.ToString();
            SeatLabel.Text = ThisTicket.seat_number.ToString();
        }

    }
}