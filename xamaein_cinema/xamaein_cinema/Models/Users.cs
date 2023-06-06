using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace xamaein_cinema.Models
{
    public class users
    {
        [PrimaryKey, AutoIncrement] public int user_id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public long phone_numb { get; set; }
        public Nullable<System.DateTime> date_birthday { get; set; }
        public string password { get; set; }
        public int admin_check { get; set; }
    }
}
