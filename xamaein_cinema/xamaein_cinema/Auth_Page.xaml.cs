using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using xamaein_cinema.Admin;
using Xamarin.Forms.Xaml;
using xamaein_cinema.Models;
using static SQLite.SQLite3;
using System.Runtime.InteropServices.ComTypes;

namespace xamaein_cinema
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Auth_Page : ContentPage
    {
        List<users> users;
        bool ChekBeck = false;
        public Auth_Page(bool Check)
        {
            ChekBeck = Check;
            InitializeComponent();
            Xamarin.Forms.NavigationPage navPage = (Xamarin.Forms.NavigationPage)App.Current.MainPage;

            Title = "Вход";


        }
        protected override void OnAppearing()
        {
            Viz(); 
        }

        protected  void Viz()
        {
            users =  App.Database.GetUsers();
        }
        private async void Auth_clck(object sender, System.EventArgs e)
        {
            users = App.Database.GetUsers();
            try
            {
                foreach (var users in users)
                {
                    if (passwordEntry.Text == users.password & Convert.ToInt64(PhoneEntry.Text) == users.phone_numb & users.admin_check == 0)
                    {
                        ThisUser.UserId = users.user_id;
                        if (ChekBeck) 
                        { 
                            Navigation.PopAsync(); 
                        }
                        else
                        {
                            this.Navigation.PushAsync(new UserTickets());
                        }
                        return;
                               

                    }
                    else if (passwordEntry.Text == users.password & Convert.ToInt64(PhoneEntry.Text) == users.phone_numb & users.admin_check == 1)
                    {                      
                        this.Navigation.PushAsync(new AdminTickets());
                        return;

                    }
                    else if (passwordEntry.Text == users.password & Convert.ToInt64(PhoneEntry.Text) == users.phone_numb & users.admin_check == 2)
                    {
                        this.Navigation.PushAsync(new MenuPage());
                        return;

                    }
                }


                await DisplayAlert("", "Логин и пароль не совпадают", "OK");
            }
            catch
            {
                await DisplayAlert("", "Erorr", "OK");
            }
            


        }
        private async void Go_Reg(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new Registr_Page());

        }
        //private void PhoneEntry_TextChanged(object sender, System.EventArgs e)
        //{
        //    if (PhoneEntry.Text == "" || PhoneEntry.Text == "+")
        //    {
        //        PhoneEntry.Text = "+7";
        //    }
            
        //}

        //private void PhoneEntry_Focused(object sender, FocusEventArgs e)
        //{
        //    if (PhoneEntry.Text == "" || PhoneEntry.Text == "+")
        //    {
        //        PhoneEntry.Text = "+7";
        //    }
        //}
    }
}