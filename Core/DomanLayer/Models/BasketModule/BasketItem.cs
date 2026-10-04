using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.BaskedModule
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}



//aspnet in memory backage uses from the devicce rescorces to store data in ram
//Redis will store on mmy server ram OR store oN Cloud Ram (Online)

// Basket hAS LifeTime Ends IF U Didn't InterACT With the Basket For Long Time 
