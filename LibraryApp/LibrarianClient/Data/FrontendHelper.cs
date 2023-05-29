using EventAggregator.Blazor;

namespace LibrarianClient.Data
{
    public static class FrontendHelper
    {
        public static async Task StartLoading(IEventAggregator eventAggregator)
        {
            await eventAggregator.PublishAsync(new LoadEvent { State = true });
            await Task.Delay(1);
        }

        public static async Task StopLoading(IEventAggregator eventAggregator)
        {
            await eventAggregator.PublishAsync(new LoadEvent { State = false });
            await Task.Delay(1);
        }
    }
}
