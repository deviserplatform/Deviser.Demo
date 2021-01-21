using System.Collections.Generic;

namespace Deviser.Demo.Admin.Models
{
    public class OrderStatus
    {
        private static IList<OrderStatus> _orderStatuses;
        public int Id { get; set; }
        public string Name { get; set; }

        public static IList<OrderStatus> OrderStatuses =>
            _orderStatuses ?? new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Id = 1,
                    Name = "Created"
                },
                new OrderStatus()
                {
                    Id = 2,
                    Name = "Submitted"
                },
                new OrderStatus()
                {
                    Id = 3,
                    Name = "Shipped"
                },
                new OrderStatus()
                {
                    Id = 4,
                    Name = "Delivered"
                }
            };
    }
}