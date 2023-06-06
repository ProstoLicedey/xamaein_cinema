using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using xamaein_cinema.Models;

namespace xamaein_cinema.Data
{

    public class CinemaAyncRepository
    {
        private readonly SQLiteConnection database;

        public CinemaAyncRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<users>();
            database.CreateTable<films>();
            database.CreateTable<session>();
            database.CreateTable<tickets>();
        }

         
        public  List<users> GetUsers()
        {
            return  database.Table<users>().ToList();

        }
        public users GetUsers(int id)
        {
            return database.Table<users>().Where(x => x.user_id == id).First();

        }
        public async Task<int> GetUsers(users item)
        {
            return  database.Delete(item);
        }
        public int SaveUsers(users item)
        {
           
                return database.Insert(item);
            
        }


        //фильмы
        public int GetFilmsName(string a)
        {
            return database.Table<films>().FirstOrDefault(x => x.name == a).film_id;
        }
        public  List<films> GetFilms()
        {
            return database.Table<films>().ToList();
            
        }
      
        public films GetFilms(int id)
        {
            return  database.Get<films>(id);
        }
        public  int GeteFilms(films item)
        {
            return database.Delete(item);
        }
        public  int SaveFilms(films item)
        {
            if (item.film_id != 0)
            {
                database.Update(item);
                return item.film_id;
            }
            else
            {
                return database.Insert(item);
            }


        }
        public int DeleteFilms(films item)
        {
            return database.Delete(item);
        }

        //сеансы
        public List<session> GetSes()
        {
            var ses =  database.Table<session>().ToList();
            for (int i = 0; i < ses.Count; i++)
            {
                films disc = database.Get<films>(ses[i].film_id);
                ses[i].nameF  = disc.name;
               
            }
            return ses;

        }
        public int DeleteSession(session item)
        {
            return database.Delete(item);
        }
        public List<session> GetSesFilm(films film)
        {
            return database.Table<session>().Where(x => x.film_id == film.film_id).ToList();
          
        }

        public session GetSes(int id)
        {
            return database.Get<session>(id);
        }
        public int GetSes(session item)
        {
            return database.Delete(item);
        }
        public int SaveSes(session item)
        {
            if (item.session_id != 0)
            {
                database.Update(item);
                return item.session_id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        //билеты

        public List<tickets> GeteTickets()
        {
            return database.Table<tickets>().ToList();

        }
        public List<tickets>  GetTicketsWhereUsers(int input)
        {
            return database.Table<tickets>().Where(x => x.user_id == input).ToList();
  

        }
        public List<tickets> GetTicketsWhereSeans(int input)
        {
            return database.Table<tickets>().Where(x => x.user_id == input).ToList();


        }
        public tickets GeteTickets(int id)
        {
            return database.Table<tickets>().Where(x => x.tickets_id == id).First();
        }
        public int DeleteTickets(tickets item)
        {
            return database.Delete(item);
        }
        public int UpdateTickets(tickets item)
        {
            return database.Update(item);
        }
        public int SaveTickets(tickets item)
        {
            //if (item.tickets_id != 0)
            //{
            //    database.Update(item);
            //    return item.tickets_id;
            //}
            //else
            //{ 
                return database.Insert(item);
            //}
        }

    }
}
