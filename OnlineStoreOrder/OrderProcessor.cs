using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OnlineStoreOrder
{
    public class OrderProcessor
    {
        public event EventHandler<Order>? OrderReadyToShip;
        public void ValidateBatch(List<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(
                    nameof(orders));
            }

            if (orders.Count < 3 || orders.Count > 15)
            {
                throw new ArgumentException("Batch must contain between 3 and 15 orders.");
            }

            var duplicateIds = orders
                .GroupBy(order => order.OrderId)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            if (duplicateIds.Count > 0)
            {
                throw new OrderValidationException(
                    duplicateIds[0],
                    $"Duplicate OrderId detected: " +
                    $"{duplicateIds[0]}.");
            }
        }

        public void ValidateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(
                    nameof(order));
            }

            if (order.ItemList == null ||
                order.ItemList.Count == 0)
            {
                throw new OrderValidationException(
                    order.OrderId);
            }

            if (order.TotalAmount <= 0)
            {
                throw new OrderValidationException(
                    order.OrderId);
            }
        }

        public void ShipOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(
                    nameof(order));
            }

            if (!order.IsPaid)
            {
                throw new InvalidOperationException(
                    $"Order {order.OrderId} has not been paid.");
            }

            if (!order.StockAvailable)
            {
                throw new InvalidOperationException(
                    $"Stock unavailable for Order {order.OrderId}.");
            }

            Console.WriteLine($"Order {order.OrderId} shipped.");
        }


        private void RaiseOrderReadyToShip(Order order)
        {
            OrderReadyToShip?.Invoke(this,order);
        }


        public string GetHandlingNotes(Order order)
        {
            List<string> notes = new();

            foreach (OrderItem item in order.ItemList)
            {
                Type itemType = item.GetType();

                HandlingRequirementAttribute?attribute =itemType.GetCustomAttribute<HandlingRequirementAttribute>();

                if (attribute != null)
                {
                    notes.Add($"{item.name}: {attribute.requirement}");
                }
            }

            if (notes.Count == 0)
            {
                return "None";
            }

            return string.Join("; ", notes);
        }


        public decimal CalculateTotalRevenue(IEnumerable<Order> orders)
        {
            return orders.Sum(order => order.TotalAmount);
        }

        public IEnumerable<IGrouping<int, Order>>GroupOrdersByCustomer(IEnumerable<Order> orders)
        {
            return orders.GroupBy(order => order.CustomerId);
        }


        public int? GetTopCustomer(IEnumerable<Order> orders)
        {
            return orders
                .GroupBy(order => order.CustomerId)
                .Select(group => new
                {
                    CustomerId = group.Key,

                    TotalSpend = group.Sum(
                        order => order.TotalAmount)
                })
                .OrderByDescending(
                    x => x.TotalSpend)
                .Select(x => (int?)x.CustomerId)
                .FirstOrDefault();
        }


        public decimal GetCustomerSpend(
            IEnumerable<Order> orders,
            int customerId)
        {
            return orders
                .Where(order =>
                    order.CustomerId == customerId)
                .Sum(order => order.TotalAmount);
        }



        public void processOrders(IEnumerable<Order>orders,
            Func<Order,decimal> discount_rule,
            Action<Order> notification,
            Predicate<Order> ShipingEligibility,
            ManifestExporter exporter)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            if (discount_rule == null)
            {
                throw new ArgumentNullException(nameof(discount_rule));
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

            List<Order> orderList= orders.ToList();
            ValidateBatch(orderList);

            List<Order> sortedList = orders.
                OrderByDescending(order => order.IsExpress).
                ThenBy(order=>order.OrderId).
                ToList();

            foreach(Order order in sortedList)
            {
                try
                {
                    ValidateOrder(order);

                    decimal discount =discount_rule(order);
                    
                    if (discount < 0)
                    {
                        discount = 0;
                    }

                    if (discount > order.TotalAmount)
                    {
                        discount = order.TotalAmount;
                    }

                    order.TotalAmount -= discount;

                    bool eligible = ShipingEligibility(order);
                    if (!eligible)
                    {
                        Console.WriteLine($"Order {order.OrderId} is not eligible for shipping.");
                        continue;

                    }

                    ShipOrder(order);

                    notification(order);

                    RaiseOrderReadyToShip(order);

                    string HandlingNotes = GetHandlingNotes(order);

                    exporter.WriteOrder(order, HandlingNotes);

                }
                catch(InvalidOperationException e)
                {
                    Console.WriteLine($"Shipping failed for Order {order.OrderId}: {e.Message}");

                }
            }

        }
    }
}
