using OnlineStoreOrder;

class Program
{
    static void Main()
    {
        
        Order p1 = new Order(id: 101,cust_id:1,items:new List<OrderItem>
            {
                new FragileItem(
                    "Glass Vase",
                    1200m),

                new NormalItem(
                    "Box",
                    100m)
            },
           amount:1300m,
            IsExpress: true,
         IsPaid: true,
            StockAvailable:true);

        Order p2 = new Order(102, 2,new List<OrderItem>
            {
                new NormalItem(
                    "Shoes",
                    2000m)
            },2000m,false,true,true);

        Order p3 = new Order(103,1,new List<OrderItem>
            {
                new FragileItem(
                    "Monitor",
                    5000m)
            },5000m,true,true,true);

        Order p4 = new Order(104,3,new List<OrderItem>
            {
                new NormalItem(
                    "Keyboard",
                    1000m)
            }, 1000m, false, false, true);

        List<Order> orders = new()
        {
            p1,
            p2,
            p3,
            p4
        };
        OrderProcessor processor =new OrderProcessor();

        Func<Order, decimal> discountRule =DiscountRule.CreateDiscountRule(minSpend: 1000m,discountPercent: 10m);

        Predicate<Order> shippingEligibility =order => order.StockAvailable && order.IsPaid;

        Action<Order> notification= order => Console.WriteLine($"EMAIL: Confirmation sent for Order {order.OrderId}.");

        processor.OrderReadyToShip += (sender, order) => Console.WriteLine($"EVENT: Order {order.OrderId} is ready to ship.");

        List<Order> manifestOrders = new();

        processor.OrderReadyToShip +=(sender, order) =>
            {
                manifestOrders.Add(order);

                Console.WriteLine($"EVENT: Order {order.OrderId} added to manifest list.");
            };

        ManifestExporter? exporter = null;

        try
        {
            exporter =new ManifestExporter("shipping-manifest.txt");
            processor.processOrders(
                orders,
                discountRule,
                notification,
                shippingEligibility,
                exporter);

            decimal revenue =processor.CalculateTotalRevenue(orders);

            Console.WriteLine();
            Console.WriteLine($"Total Revenue: {revenue}");

            Console.WriteLine();
            Console.WriteLine("Orders grouped by customer:");

            var groupedOrders =processor.GroupOrdersByCustomer(orders);

            foreach (var group in groupedOrders)
            {
                Console.WriteLine($"Customer {group.Key}:");

                foreach (Order order in group)
                {
                    Console.WriteLine($" Order {order.OrderId} Amount = {order.TotalAmount}");
                }
            }

            int? topCustomer =processor.GetTopCustomer(orders);

            Console.WriteLine();

            if (topCustomer.HasValue)
            {
                Console.WriteLine($"Top Customer: {topCustomer.Value}");
            }


            Console.WriteLine();
            Console.WriteLine($"Manifest orders: {manifestOrders.Count}");

            foreach (Order order in manifestOrders)
            {
                Console.Write( $"Manifest -> Order {order.OrderId}");
            }


            Console.WriteLine();
            Console.WriteLine("Testing unpaid shipping:");

            try
            {
                processor.ShipOrder(p4);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught expected exception: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {

            exporter?.Dispose();

            Console.WriteLine();
            Console.WriteLine("Exporter disposed in finally.");
        }

        Console.WriteLine();
        Console.WriteLine("Processing completed.");
    }
}

