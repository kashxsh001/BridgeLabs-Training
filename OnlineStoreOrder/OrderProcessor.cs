using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    internal class OrderProcessor
    {
        public event EventHandler<Order>? OrderReadyToShip;


        void processOrders(IEnumerable<Order>orders,
            Func<Order,decimal> discountrule,
            Action<Order> notification,
            Predicate<Order> ShipingEligibility,
            ManifestExporter exporter)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            if (discountrule == null)
            {
                throw new ArgumentNullException(nameof(discountrule));
            }
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }
            if (ShipingEligibility == null)
            {
                throw new ArgumentNullException(nameof(ShipingEligibility));
            }
            if (exporter == null)
            {
                throw new ArgumentNullException(nameof(exporter));
            }

            List<Order> orderList = 
        }
    }
}
