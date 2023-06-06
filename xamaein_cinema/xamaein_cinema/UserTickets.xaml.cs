using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamaein_cinema.Data;
using xamaein_cinema.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTickets : ContentPage
    {
        public List<tickets> Tickets;
        public UserTickets()
        {
            InitializeComponent();
            this.Navigation.PopToRootAsync(); // обнуляет стек, для запрета возврата обратно
            Tickets = App.Database.GetTicketsWhereUsers(ThisUser.UserId);
            NS.Text = App.Database.GetUsers(ThisUser.UserId).name;
            Phone.Text = App.Database.GetUsers(ThisUser.UserId).phone_numb.ToString();

            Generate();
        }
        private async void Generate()
        {
            await Navigation.PopAsync();
            if (Tickets.Count == 0)
            {
                Label label = new Label
                {
                    Text = "У вас пока нет билетов",
                    TextColor = Color.White,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Italic,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = 10
                    
                };
                Bilets.Children.Add(label);
            }
            else
            {
                for (int i = 0; i < Tickets.Count; i++)
                {
                    session ThisSes = App.Database.GetSes(Tickets[i].session_id);
                    films ThisFilm = App.Database.GetFilms(ThisSes.film_id);

                    StackLayout mainStackLayout = new StackLayout
                    {
                        ClassId = Tickets[i].tickets_id.ToString(),
                        Margin = new Thickness(0, 20, 0, 0),
                        Children =
                {

                    new StackLayout
                    {

                         ClassId = ThisSes.session_id.ToString(),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                             
                   
                    new Xamarin.Forms.Image
                            {
                                Source = ThisFilm.image,
                                HeightRequest = 120,
                                WidthRequest = 80,
                                Aspect = Aspect.Fill,
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                Margin = new Thickness(10,0,0,0)
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                Margin = new Thickness(-80,0,0,0),
                                Children =
                                {
                                    new Label
                                    {
                                        Text = ThisFilm.name,
                                        TextColor = Color.White,
                                        FontAttributes = FontAttributes.Bold,
                                        FontSize = 22,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    },
                                    new Label
                                    {
                                        Text = ThisSes.date_time,
                                        TextColor = Color.White,
                                        FontSize = 17,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        VerticalOptions = LayoutOptions.EndAndExpand,
                                        Margin = new Thickness(0,0,0,5),
                                        Children =
                                        {
                                            new Label
                                            {
                                                Text = "Ряд " + Tickets[i].row_number.ToString(),
                                                TextColor = Color.White,
                                                FontSize = 17,
                                                FontAttributes = FontAttributes.Italic,
                                                HorizontalOptions = LayoutOptions.StartAndExpand
                                            },
                                            new Label
                                            {
                                                Text = "Место " + Tickets[i].seat_number.ToString(),
                                                TextColor = Color.White,
                                                FontSize = 17,
                                                FontAttributes = FontAttributes.Italic,
                                                HorizontalOptions = LayoutOptions.StartAndExpand
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new BoxView
                    {
                        BackgroundColor = Color.White,
                        Opacity = 0.2,
                        HeightRequest = 1,
                        VerticalOptions = LayoutOptions.End
                    }
                }
                    };
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoBilet;
                    Label labelId = new Label
                    {

                        ClassId = "labelID",
                        Text = Tickets[i].tickets_id.ToString(),
                        IsVisible = false
                    };
                    mainStackLayout.Children.Add(labelId);
                   mainStackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                    Bilets.Children.Add(mainStackLayout);
                }
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MainPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ThisUser.UserId = 0;
            this.Navigation.PushAsync(new MainPage());
        }
        private void GoBilet(object sender, EventArgs e)
        {
            var stackLayout = sender as StackLayout;
            if (stackLayout != null)
            {
                var labelID = stackLayout.Children.FirstOrDefault(c => c is Label && ((Label)c).ClassId == "labelID") as Label;
                if (labelID != null)
                {
                    int labelText = Convert.ToInt32(labelID.Text);
                    this.Navigation.PushAsync(new TicketPage(labelText));
                }
            }
           
        }
    }
}