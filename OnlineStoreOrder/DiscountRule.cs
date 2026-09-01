using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    internal class DiscountRule
    {
        public Func<Order,decimal> CreateDiscountRule(decimal minSpend, decimal discountPercent)
        {
            return order =>
            {
                if (order.TotalAmount >= minSpend)
                {
                    return order.TotalAmount * discountPercent / 100m;
                }
                return 0m;
            };
           
        }
    }
}
