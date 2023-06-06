using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamaein_cinema.Models;
using xamaein_cinema.Data;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.Identity.Client;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Seans_Page : ContentPage
    {

        public List<session> Ses1;
        public List<session> Ses2 = new List<session>();
        public session SelectedSes { get; set; }
        public films Films1 { get; set; }
        public Seans_Page(films Films)
        {
            InitializeComponent();
            Films1 = Films;
            NameFilm.Text = Films.name;
            AgeFilm.Text = Films.age_restriction;
            DiscFilm.Text = Films.description;
            ImageFilm.Source = Films.image;

            Ses1 = App.Database.GetSesFilm(Films);

            picker.SelectedIndex = 0;
            int count = 1;
            List<string> dateList = new List<string>();
            if(Ses1.Count == 0)
            {
                var label1 = new Label
                {

                    Text = "Извините, на данный фильм пока нет  доступных сеансов",
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 14,
                    Margin = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                vibors.Children.Add(label1);
            }

            for (int i = 0; i < Ses1.Count; i++)
            {
                bool flag = false;
                int countP = 0;
                string date = "";

                for (int k = 0; k < Ses1[i].date_time.Length; k++)
                {

                    if (Ses1[i].date_time[k] == ' ')
                    {
                        countP++;
                    }
                    if (countP != 2)
                    {
                        date += Ses1[i].date_time[k];
                    }
                    else
                    {
                        break;
                    }
                }

                for (int j = 0; j < dateList.Count; j++)
                {

                    if (date == dateList[j])
                    {
                        flag = true;
                    }

                }
                if (flag == false)
                {
                    picker.Items.Add(date);
                    dateList.Add(date);
                    count++;
                }

            }

        }
        public films SelectedFilms { get; set; }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var stackLayout = sender as StackLayout;
            if (stackLayout != null)
            {
                var labelID = stackLayout.Children.FirstOrDefault(c => c is Label && ((Label)c).ClassId == "labelID") as Label;
                if (labelID != null)
                {
                    int labelText = Convert.ToInt32(labelID.Text);
                    SelectedSes = App.Database.GetSes(labelText);
                    this.Navigation.PushAsync(new Zal_Page(SelectedSes, Films1));
                }
            }
        }

        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (picker.SelectedIndex != 0)
            {
                vibors.Children.Clear();
                Ses2.Clear();
                for (int i = 0; i < Ses1.Count; i++)
                {
                    string date = "";
                    int countP = 0;
                    for (int k = 0; k < Ses1[i].date_time.Length; k++)
                    {

                        if (Ses1[i].date_time[k] == ' ')
                        {
                            countP++;
                        }
                        if (countP != 2)
                        {
                            date += Ses1[i].date_time[k];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (date == picker.SelectedItem.ToString())
                    {
                        Ses2.Add(Ses1[i]);
                    }

                }
            }
            
     

             
          

            for (int i = 0; i < Ses2.Count; i++)
            {

                var frame = new Frame
                {
                    BorderColor = Color.White,
                    CornerRadius = 5,
                    BackgroundColor = Color.FromHex("#1B1A28"),
                    WidthRequest = 90,
                    HeightRequest = 60,
                    Margin = new Thickness(10),
                    Padding = new Thickness(0, 5, 0, 5)
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
              
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += Button_Clicked;
               
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                int countP = 0;
                string time = "";
                for (int k = Ses2[i].date_time.Length - 5; k < Ses2[i].date_time.Length; k++)
                {
                    time += Ses2[i].date_time[k];
                }


                var label1 = new Label
                {
                   
                    Text = time,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20,
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                var label2 = new Label
                {
                    Text = Ses2[i].price + "₽",
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                var labelID = new Label
                {
                   
                    ClassId = "labelID",
                    Text = Ses2[i].session_id.ToString(),
                    IsVisible = false
                };
                stackLayout.Children.Add(labelID);
                stackLayout.Children.Add(label1);
                stackLayout.Children.Add(label2);

                frame.Content = stackLayout;
                vibors.Children.Add(frame);
            }
        }
    }
}