using Microsoft.AspNet.SignalR.Client;
using NotificationService.Data;
using NotificationService.Data.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace NotificationService.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupHubPublisher();
        }

        private static void SetupHubPublisher()
        {
            var hubLocation = ConfigurationManager.AppSettings["SignalRHubUri"];
            var hubConnection = new HubConnection(hubLocation);
            var hubProxy = hubConnection.CreateHubProxy("OrderHub");

            hubProxy.On<Order>("createdOrder", order => Console.WriteLine("{0}: {1} {2}", order.OrderDate, order.Customer.FirstName, order.Customer.LastName));
            hubConnection.Start().Wait();

            while (true)
            {
                var orders = DataContext.Orders;
                foreach (var order in orders)
                {
                    var message = String.Format("{0} {1} ordered {2} item(s)", order.Customer.FirstName, order.Customer.LastName, order.OrderItems.Count());
                    hubProxy.Invoke<Order>("SendOrder", message, order);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
