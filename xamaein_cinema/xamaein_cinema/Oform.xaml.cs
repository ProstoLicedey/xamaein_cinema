using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.Profile;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Oform : ContentPage
    {
        public List<Mest> MestList = new List<Mest>();
        public session Seans;
        public films Film;
        public Oform(session Ses, films Fil, List<Mest> MList)
        {
            InitializeComponent();

            Seans = Ses;
            Film = Fil;
            MestList = MList;

            NameFilm.Text = Film.name;
            AgeFilm.Text = Film.age_restriction;
            DateSes.Text = Ses.date_time;

            ZalSes.Text = Ses.number_zal.ToString();
            FilmImage.Source = Film.image;

            int sum = (int)(MestList.Count * Ses.price);
            for (int i = 0; i < MestList.Count; i++)
            {

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(8),
                };
                var r1Label = new Label
                {
                    TextColor = Color.White,
                    Text = "Ряд " + MestList[i].read1,
                    Margin = new Thickness(0, 0, 10, 10),
                    FontSize = 20,
                    AutomationId = "r1"
                };
                stackLayout.Children.Add(r1Label);
                var m1Label = new Label
                {
                    TextColor = Color.White,
                    Text = "Место " + MestList[i].mest1,
                    Margin = new Thickness(0, 0, 0, 10),
                    FontSize = 20,
                    AutomationId = "m1"
                };
                stackLayout.Children.Add(m1Label);
                var prise1Label = new Label
                {
                    TextColor = Color.White,
                    Text = Ses.price.ToString() + " ₽",
                    FontSize = 20,
                    Margin = new Thickness(120, 0, 0, 10),
                    HorizontalOptions = LayoutOptions.End,
                    AutomationId = "Prise1"
                };
                stackLayout.Children.Add(prise1Label);
                var boxView = new BoxView
                {
                    BackgroundColor = Color.White,
                    Opacity = 0.2,
                    HeightRequest = 1,
                    VerticalOptions = LayoutOptions.End
                };
                StackMest.Children.Add(stackLayout);
                StackMest.Children.Add(boxView);

            }
            Label_sum.Text = "Итого:" + sum + "₽";
        }


        private void Oform_clck(object sender, EventArgs e)
        {
            Random random= new Random();
            int Tid = random.Next(0, 3000);
            for (int i = 0; i < MestList.Count; i++)
            {
                tickets Ticket = new tickets
                {
                    tickets_id = Tid,
                    session_id = Seans.session_id,
                    row_number = MestList[i].read1,
                    seat_number = MestList[i].mest1,
                    user_id = ThisUser.UserId,
                    check_control = false
                };
                Tid++;

                App.Database.SaveTickets(Ticket);
            }
             this.Navigation.PushAsync(new UserTickets());

        }
    }
}