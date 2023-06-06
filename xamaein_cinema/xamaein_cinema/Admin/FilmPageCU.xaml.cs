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
    public partial class FilmPageCU : ContentPage
    {
    
        films Films;
        public FilmPageCU(films films)
        {
            InitializeComponent();
      
            Films = films;

            if (films != null)
            {
                NameEntry.Text = films.name;
                DiscEntry.Text = films.description;
                AgeEntry.Text = films.age_restriction;
                ImageEntry.Text = films.image;
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            try { 
            if (Films == null)
                {
                Random random = new Random();
                    films films = new films
                    {
                       // film_id= random.Next(100,9999),
                        name = NameEntry.Text,
                        description = DiscEntry.Text,
                        age_restriction = AgeEntry.Text + "+",
                        image = ImageEntry.Text
                    };

                    App.Database.SaveFilms(films);
                }
            else
            {
                Films.name = NameEntry.Text;
                Films.description = DiscEntry.Text;
                Films.age_restriction = AgeEntry.Text + "+";
                Films.image = ImageEntry.Text;
                App.Database.SaveFilms(Films);

            }


            var T = App.database.GetFilms();

            this.Navigation.PopAsync();
        }
            catch { }
            
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
           
                if (Films != null)
                {
                    
                    App.Database.DeleteFilms(Films);
                }
                this.Navigation.PopAsync();
            
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}