using Caliburn.Micro;
using NotificationService.Data;
using NotificationService.Data.Models;
using NotificationService.Desktop.Messages;
using System.Collections.ObjectModel;
using System.Linq;

namespace NotificationService.Desktop.ViewModels
{
    public class OrdersViewModel
        : Screen
    {
        private IEventAggregator eventAggregator { get; set; }

        public OrdersViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            base.DisplayName = "Orders List";
        }

        protected override void OnActivate()
        {
            var orders = DataContext.Orders
                .Select(o => new OrdersGridItem()
                {
                    OrderDate = o.OrderDate,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    TotalItemCount = o.OrderItems.Count(),
                    TotalItemPrice = o.OrderItems.Select(oi => oi.Item.Price * oi.Quantity).Sum(),
                });

            Orders = new ObservableCollection<OrdersGridItem>(orders);

            base.OnActivate();
        }

        public void GoBack()
        {
            eventAggregator.Publish(new NavigateToTileMessage());
        }

        public ObservableCollection<OrdersGridItem> Orders { get; set; }
    }
}
