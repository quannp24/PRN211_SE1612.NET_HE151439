using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slot25_WebApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitInStock { get; set; }
    }
}
