using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> ItemList { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsExpress { get; set; }

        public bool IsPaid { get; set; }

        public bool StockAvailable { get; set; }

        public Order(
            int id,
            int cust_id,
            List<OrderItem> items,
            decimal amount,
            bool IsExpress,
            bool IsPaid,
            bool StockAvailable)
        {
            OrderId = id;
            CustomerId = cust_id;
            ItemList = items;
            TotalAmount = amount;
            this.IsExpress = IsExpress;
            this.IsPaid = IsPaid;
            this.StockAvailable = StockAvailable;
        }

    }
}
