using Caliburn.Micro;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using NotificationService.Data.Models;
using NotificationService.Desktop.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace NotificationService.Desktop.ViewModels
{
    public interface IShellViewModel { }

    public class ShellViewModel
        : Conductor<Screen>.Collection.OneActive, IHandle<NavigateToOrdersMessage>, IHandle<NavigateToTileMessage>, IShellViewModel
    {
        public ShellViewModel()
        {
            OrderMessages = new ObservableCollection<Order>(new List<Order>());
            Notifications = new ObservableCollection<string>(new List<string>());
            base.DisplayName = "Signal R Demo";
        }

        private void SetupHubListener()
        {
            var hubLocation = ConfigurationManager.AppSettings["SignalRHubUri"];
            var hubConnection = new HubConnection(hubLocation);
            var hubProxy = hubConnection.CreateHubProxy("OrderHub");

            hubConnection.Start().Wait();
            hubProxy.On("createdOrder", (orderJson) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var orderJsonString = orderJson.ToString();
                    var order = JsonConvert.DeserializeObject<Order>(orderJsonString);
                    OrderMessages.Add(order);
                });
            });

            hubProxy.On("notify", (messageObject) =>
            {
                var message = Convert.ToString(messageObject);
                App.Current.Dispatcher.Invoke(() =>
                {
                    Notifications.Add(message);
                });
            });
        }

        protected override void OnActivate()
        {
            SetupHubListener();

            this.ActivateItem(IoC.Get<TileViewModel>());
            base.OnActivate();
        }

        public void Handle(NavigateToOrdersMessage message)
        {
            this.ActivateItem(IoC.Get<OrdersViewModel>());
        }

        public void Handle(NavigateToTileMessage message)
        {
            this.ActivateItem(IoC.Get<TileViewModel>());
        }

        public ObservableCollection<Order> OrderMessages { get; set; }

        public ObservableCollection<string> Notifications { get; set; }
    }
}