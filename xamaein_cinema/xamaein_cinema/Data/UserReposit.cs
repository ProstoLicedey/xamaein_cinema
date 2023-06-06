using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using xamaein_cinema.Models;

namespace xamaein_cinema.Data
{
    
   public class UsersReposit { 
        SQLiteConnection database;
        public UsersReposit(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Users>();
        }
        public IEnumerable<Users> GetItems()
        {
            return database.Table<Users>().ToList();
        }
        public Users GetItem(int id)
        {
            return database.Get<Users>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Users>(id);
        }
        public int SaveItem(Users item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
