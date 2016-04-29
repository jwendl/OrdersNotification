using System;

namespace NotificationService.Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Location ShippingAddress { get; set; }

        public Location BillingAddress { get; set; }
    }
}
