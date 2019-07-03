using System;
using System.Linq;

namespace RogalskiJaroslaw.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OrdersContext context)
        {
            context.Database.EnsureCreated();

            if (context.Orders.Any())
            {
                return;
            }

            var orders = new Orders[]
            {
            new Orders{OrderId = 1, OrderNumber = "abc123",  EstimatedDelivery = DateTime.Now, OrderComments = "aaa", OrderDate = DateTime.Today,  OrderOrigin = "zxc"},
            new Orders{OrderId = 2, OrderNumber = "2asd",
                EstimatedDelivery = DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderComments = "aaa",
                OrderDate = DateTime.ParseExact("2019-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderOrigin = "zxc"},
            new Orders{OrderId = 3, OrderNumber = "3asd",
                EstimatedDelivery = DateTime.ParseExact("2007-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderComments = "aaa",
                OrderDate = DateTime.ParseExact("2029-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderOrigin = "zxc"},
            new Orders{OrderId = 4, OrderNumber = "4asd",
                EstimatedDelivery = DateTime.ParseExact("2000-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderComments = "aaa",
                OrderDate = DateTime.ParseExact("2002-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture),
                OrderOrigin = "zxc"},

            };
            foreach (Orders e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
        }
    }
}
