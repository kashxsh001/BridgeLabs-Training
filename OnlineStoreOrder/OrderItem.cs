using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    public class OrderItem
    {
        public string name { get; set; }
        public decimal price{get; set;}

        public OrderItem(string name,decimal price)
        {
            this.name = name;
            this.price = price;
        }

    }
}
