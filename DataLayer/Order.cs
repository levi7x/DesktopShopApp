using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Order
    {
        public int Id_o { get; set; }
        //public int Id_c { get; set; }
        public int Id_u { get; set; }
        public bool Canceled { get; set; }
        public DateTime Time { get; set; }
        public String Notification { get; set; }
        public int NumOrder { get; set; }
    }
}
