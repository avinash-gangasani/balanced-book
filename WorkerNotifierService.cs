using BalancedBook.NET.Models;

namespace BalancedBook.NET
{
    public class WorkerNotifierService
    {
        // Event that the Blazor component will subscribe to
        public event Action<Order>? OnJobExecuted;

        // Method the BackgroundWorker will call to raise the event
        public void NotifyJobExecuted(Order order)
        {
            OnJobExecuted?.Invoke(order);
        }
    }
}
