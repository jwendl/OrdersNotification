using System;

namespace NotificationService.Data.Models
{
    public class OrdersGridItem
    {
        public DateTime OrderDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalItemCount { get; set; }

        public decimal TotalItemPrice { get; set; }
    }
}
