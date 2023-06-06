using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.Profile;

namespace xamaein_cinema.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeansPageCU : ContentPage
    {
        public ObservableCollection<string> Data;
        session Session;
        public SeansPageCU(session session)
        {
            InitializeComponent();
            Session= session;

            Data = new ObservableCollection<string>();
            var films = App.database.GetFilms();
            foreach (var film in films)
            {
                Data.Add(film.name);
            }

            BindingContext = this;
            ro.ItemsSource = Data;
            if (session != null)
            {

                DateB.Date = DateTime.Parse(session.date_time);
                TimeEntry.Time = DateTime.Parse(session.date_time).TimeOfDay;
                ZalEntry.Text = session.number_zal.ToString();
                PriceEntry.Text = session.price.ToString();
            }
        }

        protected override void OnAppearing()
        {
            ShowNews();
        }
        private void ShowNews()
        {
           
            //if (Class1.flag1 == 0)
            //{
            //    fi.SelectedItem = Class1.ud.fio_u;
            //    na.SelectedItem = Class1.ud.name_d;
            //}
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Session == null)
                {
                    session session = new session
                    {

                        price = Convert.ToInt32(PriceEntry.Text),
                        number_zal = Convert.ToInt32(ZalEntry.Text),
                        date_time = DateB.Date.ToString("d MMMM  yyyy") + TimeEntry.Time.ToString("hh:mm"),
                        film_id = App.database.GetFilmsName(ro.SelectedItem.ToString())
                    };

                    App.Database.SaveSes(session);
                }
                else
                {

                    Session.price = Convert.ToInt32(PriceEntry.Text);
                    Session.number_zal = Convert.ToInt32(ZalEntry.Text);
                    Session.date_time = DateB.Date.ToString("d MMMM yyyy") + TimeEntry.Time.ToString("t");
                    Session.film_id = App.database.GetFilmsName(ro.SelectedItem.ToString());


                    App.Database.SaveSes(Session);

                }
            }
            catch { }


            this.Navigation.PopAsync();

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            if (Session != null)
            {

                App.Database.DeleteSession(Session);
            }
            this.Navigation.PopAsync();

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

      
    }
}