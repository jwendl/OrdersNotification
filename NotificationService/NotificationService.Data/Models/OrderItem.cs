using System;

namespace NotificationService.Data.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }
}
