using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamaein_cinema.Models
{
    public class tickets
    {
         public int tickets_id { get; set; }
        public int session_id { get; set; }
        public int row_number { get; set; }
        public int seat_number { get; set; }
        public int user_id { get; set; }
        public bool check_control { get; set; }

       
    }
}
