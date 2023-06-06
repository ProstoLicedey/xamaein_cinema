using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamaein_cinema.Models
{
   public class session
    {
        [PrimaryKey, AutoIncrement] public int session_id { get; set; }
        public int film_id { get; set; }
        public decimal price { get; set; }
        public string date_time { get; set; }
        public int number_zal { get; set; }
      
        [Ignore]
        public string nameF { get; set; }
  
       
    }
}
