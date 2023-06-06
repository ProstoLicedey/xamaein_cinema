using SkiaSharp;
using SQLite;
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
    public partial class Zal_Page : ContentPage
    {
        public List<Mest> MestList = new List<Mest>();
        public session Seans;
        public films Film;
        public Zal_Page(session Ses, films Fil)
        {
            Seans = Ses;
            Film = Fil;
            InitializeComponent();
            InitButtons();
         

            Seans = Ses;
            Film = Fil;

            NameFilm.Text = Film.name;
            AgeFilm.Text = Film.age_restriction;
            DateSes.Text = Ses.date_time;
            PriceSes.Text = Ses.price.ToString() + "₽";
            ZalSes.Text = Ses.number_zal.ToString();
            FilmImage.Source = Film.image;


        }
        public int col_vibor = 0; //количество выбранных мест

        public static int read = 10;
        public static int mest = 10;

        private Button[,] btn = new Button[read, mest];
     

            public void InitButtons()
        {

            for (int i = 0; i < read; i++)
            {

                List<tickets> ticketsList;
                ticketsList = App.Database.GetTicketsWhereSeans(Seans.session_id);
             
                StackLayout stackl = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 3,
                    HorizontalOptions = LayoutOptions.Center,
                };
                Label label = new Label()
                {
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold | FontAttributes.Italic,
                    Text = (i + 1).ToString(),
                    WidthRequest = 17
                };
                stackl.Children.Add(label);
                for (int j = 0; j < mest; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].CornerRadius = 7;
                    btn[i, j].HeightRequest = 30;
                    btn[i, j].WidthRequest = 30;

                    for (int n = 0; n < ticketsList.Count; n++)
                    {
                        if (ticketsList[n].row_number == i+1 & ticketsList[n].seat_number  == j+1)
                        {
                            btn[i, j].BackgroundColor = Color.DarkRed;
                            break;
                        }
                        else
                        {
                            btn[i, j].BackgroundColor = Color.FromRgba(51, 130, 75, 255);
                        }
                    }
                    if(ticketsList.Count == 0)
                    {
                        btn[i, j].BackgroundColor = Color.FromRgba(51, 130, 75, 255);
                    }
                   
                    btn[i, j].Clicked += mest_clck;
                    stackl.Children.Add(btn[i, j]);
                }
                Mests.Children.Add(stackl);
            }
        }
        public async void mest_clck(object sender, EventArgs e)
        {
            if(ThisUser.UserId== 0)
            {
                await DisplayAlert("Внимание", "Для продолжения оформления необходимо войти в аккаунт", "Войти");
                this.Navigation.PushAsync(new Auth_Page(true));
            }
            if (col_vibor == 5)
            {
                await DisplayAlert("Извините!", "Одновременно можно выбрать не более 5 мест ", "OK");
            }
           else  if ((sender as Button).BackgroundColor == Color.FromRgba(51, 130, 75, 255) & col_vibor < 5)//если зеленая
            {

                (sender as Button).BackgroundColor = Color.FromRgba(254, 230, 0, 255);
                col_vibor++;
                Label_mest.Text = "Выбранных мест: " + col_vibor;
                for (int i = 0; i < read; i++)
                {
                    for (int j = 0; j < mest; j++)
                    {
                        if ((sender as Button) == btn[i, j])
                        {
                            Mest mest = new Mest
                            {
                                read1 = i + 1,
                                mest1 = j + 1
                            };
                            MestList.Add(mest);
                             
                            //NameFilm.Text = "Ряд " + (i + 1) + "Место" + (j + 1);
                        }
                    }
                }
            }
            else if ((sender as Button).BackgroundColor == Color.FromRgba(254, 230, 0, 255))//отмена желтости
            {
                (sender as Button).BackgroundColor = Color.FromRgba(51, 130, 75, 255);
                col_vibor--;
                Label_mest.Text = "Выбранных мест: " + col_vibor;
                
                for (int i = 0; i < read; i++)
                {
                    for (int j = 0; j < mest; j++)
                    {
                        if ((sender as Button) == btn[i, j])
                        {
                            MestList.RemoveAll(MestList => MestList.mest1 == j + 1 & MestList.read1 == i + 1);
                            }
                    }
                }
            }
        }

        private async void Oform_clck(object sender, EventArgs e)
        {
            if(MestList.Count != 0)
            {
                this.Navigation.PushAsync(new Oform(Seans, Film, MestList));
            }
            else
            {
                await DisplayAlert("Пожалуйста", "Выберите места ", "OK");
            }
        }
    }
    public class Mest
    {  
        public int read1 { get; set; }
        public int mest1 { get; set; }

    }
}