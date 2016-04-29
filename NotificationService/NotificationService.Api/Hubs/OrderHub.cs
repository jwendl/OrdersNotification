using Microsoft.AspNet.SignalR;
using NotificationService.Data.Models;
using System;

namespace NotificationService.Api.Hubs
{
    public class OrderHub
        : Hub
    {
        public void SendOrder(string message, Order order)
        {
            var outboundMessage = message;
            var instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
            if (instanceId != null)
            {
                outboundMessage = String.Format("Node Id {0} says: {1}.", instanceId, message);
            }

            Clients.All.notify(outboundMessage);
            Clients.All.createdOrder(order);
        }
    }
}