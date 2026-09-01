using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    internal class OrderValidationException:Exception
    {
        public int Order_Id { get; }
        public OrderValidationException(int order_id):base($"Invalid Order id : {order_id}")
        {
            Order_Id = order_id;
        }
        public OrderValidationException(int order_id,string message) : base(message)
        {
            Order_Id = order_id;
        }
    }
}
