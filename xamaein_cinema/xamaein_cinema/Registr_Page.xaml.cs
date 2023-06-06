using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamaein_cinema.Models;
using System.Globalization;

namespace xamaein_cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registr_Page : ContentPage
    {
        List<users> users;
        public Registr_Page()
        {
            //InitializeComponent();
            //DateB.MaximumDate = DateTime.Now.AddDays(0);
        }
        protected override void OnAppearing()
        {
            Viz();
        }

        protected  void Viz()
        {
            users = App.Database.GetUsers();
        }
        private async void Reg_clck(object sender, System.EventArgs e)
        {
            bool checkPas = CheckPassword(passwordEntry.Text);
            bool checkSurName = IsNameOrSurname(SurNameEntry.Text);
            bool checkName = IsNameOrSurname(NameEntry.Text);
            if (passwordEntry.Text != passwordEntry2.Text)
            {
                await DisplayAlert("", "Пароли не совпадают", "OK");
            }
            else if (!checkPas) 
                {
                    await DisplayAlert("Требования к паролю", " Длина пароля должна быть не менее 8 символов\r\n Пароль должен состоять из букв латинского алфавита (A-z), арабских цифр (0-9) и специальных символов", "OK");
                }
            else if (!checkSurName || !checkSurName)
            {
                await DisplayAlert("Ошибка в имени или фамилие", "Пожалуйста введите коректые фамилию и имя", "OK");
            }
            else if (PhoneEntry.Text == null)
                {
                    await DisplayAlert("", "Заполните пожалуйста все поля", "OK");
                }
                else
                {
                    Random random = new Random();
                    users Users = new users
                    {
                        //user_id = random.Next(30, 10000),
                        surname = SurNameEntry.Text,
                        name = NameEntry.Text,
                        phone_numb = Convert.ToInt64(PhoneEntry.Text),
                        date_birthday = DateB.Date,
                        password = passwordEntry.Text,
                        admin_check = 0

                    };
                    App.Database.SaveUsers(Users);
                    await DisplayAlert("Добро Пожаловать", "Регистрация прошла успешно", "OK");
                    await Navigation.PopAsync();
                }
            
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            else
            {
                bool hasLetter = false;
                bool hasDigit = false;
                bool hasSpecialChar = false;

                foreach (char c in password)
                {
                    if (char.IsLetter(c))
                    {
                        hasLetter = true;
                    }
                    else if (char.IsDigit(c))
                    {
                        hasDigit = true;
                    }
                    else if (char.IsPunctuation(c) || char.IsSymbol(c))
                    {
                        hasSpecialChar = true;
                    }
                }

                return hasLetter && hasDigit && hasSpecialChar;
            }
        }

            public bool IsNameOrSurname(string nameOrSurname)
            {
                bool isNameOrSurname = false;
                Encoding encoding = Encoding.UTF8;
                CultureInfo culture = new CultureInfo("ru-RU");

                string titleCased = culture.TextInfo.ToTitleCase(nameOrSurname.ToLower());
                byte[] bytes = encoding.GetBytes(titleCased);

                if (titleCased.All(char.IsLetter))
                {
                    isNameOrSurname = true;
                }

                return isNameOrSurname;
        }
        
    }
}