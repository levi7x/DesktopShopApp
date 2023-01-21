using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Payment
    {
        public int Id_p { get; set; }
        public int Id_o { get; set; }
        public string CardNum { get; set; }
        public int CVC { get; set; }
        public string MMYYYY { get; set; }
    }
}
