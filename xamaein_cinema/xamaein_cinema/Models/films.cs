using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamaein_cinema.Models
{
    public class films
    {
        [PrimaryKey, AutoIncrement] public int film_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string age_restriction { get; set; }
        public string image { get; set; }
    }
}
