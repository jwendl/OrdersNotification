using Caliburn.Micro;
using NotificationService.Desktop.Messages;

namespace NotificationService.Desktop.ViewModels
{
    public class TileViewModel
        : Screen
    {
        private IEventAggregator eventAggregator { get; set; }

        public TileViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            base.DisplayName = "Main Menu Page";
        }

        public void NavigateToOrders()
        {
            eventAggregator.Publish(new NavigateToOrdersMessage());
        }
    }
}
