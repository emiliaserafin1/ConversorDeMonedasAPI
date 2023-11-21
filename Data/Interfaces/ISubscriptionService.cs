using ConversorDeMonedasBack.Entities;

namespace ConversorDeMonedasBack.Data.Interfaces
{
    public interface ISubscriptionService
    {
        Subscription GetSubscriptionById(int subscriptionId);
    }
}
