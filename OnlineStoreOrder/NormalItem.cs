using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    internal class NormalItem : OrderItem
    {
        public NormalItem(string name, decimal price) : base(name, price)
        {
        }
    }
}
