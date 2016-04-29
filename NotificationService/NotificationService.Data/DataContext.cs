using FakeData;
using NotificationService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationService.Data
{
    public class DataContext
    {
        public static IEnumerable<Order> Orders
        {
            get
            {
                return GenerateRandomOrders();
            }
        }

        public static IEnumerable<Item> Items
        {
            get
            {
                return new List<Item>() {
                    new Item() { Id = Guid.NewGuid(), Name = "Minecraft", Description = "Play minecraft with friends.", Price = 26.95m },
                    new Item() { Id = Guid.NewGuid(), Name = "Visual Studio", Description = "Build awesome software.", Price = 1199m },
                };
            }
        }

        private static IEnumerable<Order> GenerateRandomOrders()
        {
            var orderCount = NumberData.GetNumber(25);
            var orders = new List<Order>();
            for (int x = 0; x < orderCount; x++)
            {
                var order = new Order();
                order.Id = Guid.NewGuid();
                order.Customer = GenerateRandomCustomer();
                order.OrderItems = GenerateRandomOrderItems();
                order.OrderDate = DateTimeData.GetDatetime();
                orders.Add(order);
            }

            return orders;
        }

        private static IEnumerable<OrderItem> GenerateRandomOrderItems()
        {
            var itemCount = NumberData.GetNumber(50);
            var orderItems = new List<OrderItem>();
            for (int x = 0; x < itemCount; x++)
            {
                var orderItem = new OrderItem()
                {
                    Id = Guid.NewGuid(),
                    Item = Items.OrderBy(i => Guid.NewGuid()).First(),
                    Quantity = NumberData.GetNumber(25),
                };
                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        private static Customer GenerateRandomCustomer()
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = NameData.GetFirstName(),
                LastName = NameData.GetSurname(),
                BirthDate = DateTimeData.GetDatetime(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-15)),
                ShippingAddress = new Location()
                {
                    AddressLine1 = PlaceData.GetAddress(),
                    City = PlaceData.GetCity(),
                    State = PlaceData.GetState(),
                    ZipCode = PlaceData.GetZipCode(),
                },
            };

            var randomNumber = NumberData.GetNumber(100);
            if (randomNumber % 2 == 0)
            {
                customer.BillingAddress = customer.ShippingAddress;
            }
            else
            {
                customer.BillingAddress = new Location()
                {
                    AddressLine1 = PlaceData.GetAddress(),
                    City = PlaceData.GetCity(),
                    State = PlaceData.GetState(),
                    ZipCode = PlaceData.GetZipCode(),
                };
            }

            return customer;
        }
    }
}
