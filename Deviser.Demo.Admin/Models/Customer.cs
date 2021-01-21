using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Deviser.Demo.Admin.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Country ShipCountry { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal Freight { get; set; }

        public string OrderStatusClass
        {
            get
            {
                if (OrderStatus == null) return string.Empty;

                return OrderStatus.Name switch
                {
                    "Created" => "badge-primary",
                    "Submitted" => "badge-info",
                    "Shipped" => "badge-warning",
                    "Delivered" => "badge-success",
                    _ => string.Empty
                };
            }
        }

    }
}
