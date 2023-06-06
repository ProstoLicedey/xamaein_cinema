using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using xamaein_cinema.Data;
using xamaein_cinema.Models;
using System.IO;
using System.Reflection;

namespace xamaein_cinema
{
    public partial class App : Application
    {

        public const string DATABASE_NAME = "PhCinema2.db";
        public static CinemaAyncRepository database;
        public static CinemaAyncRepository Database
        {
            //get
            //{
            //    if (database == null)
            //    {
            //        // путь, по которому будет находиться база данных
            //        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);

            //        // если база данных не существует (еще не скопирована)
            //        if (!File.Exists(dbPath))
            //        {
            //            // получаем текущую сборку
            //            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            //            // берем из нее ресурс базы данных и создаем из него поток
            //            using (Stream stream = assembly.GetManifestResourceStream($"xamaein_cinema.{DATABASE_NAME}"))
            //            {
            //                using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
            //                {
            //                    stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
            //                    fs.Flush();
            //                }
            //            }
            //        }
            //        database = new UsersAyncRepository(dbPath);
            //    }
            //    return database;
            //}
            get
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PhCinema2.db3");
                if (database == null)
                {

                    // если база данных не существует (еще не скопирована)
                    //if (!File.Exists(dbPath))
                    //{
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"xamaein_cinema.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                database = new CinemaAyncRepository(dbPath);
                                fs.Flush();
                            }
                        }
                    }
                    else
                    {
                        database = new CinemaAyncRepository(dbPath);
                    }
                //}
                //else
                //{
                    database = new CinemaAyncRepository(dbPath);
                //}

                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Shapes_Experimental" });
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
