using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACT.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string tenMon { get; set; }
        public string AnhMon { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal tongTien
        {
            get { return Gia * SoLuong; }
        }
    }
}