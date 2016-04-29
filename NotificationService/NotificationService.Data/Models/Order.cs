using System;
using System.Collections.Generic;

namespace NotificationService.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Customer Customer { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
