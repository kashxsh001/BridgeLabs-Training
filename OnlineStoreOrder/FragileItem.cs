using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    internal class FragileItem : OrderItem
    {
        public FragileItem(string name, decimal price) : base(name, price)
        {
        }
    }
}
