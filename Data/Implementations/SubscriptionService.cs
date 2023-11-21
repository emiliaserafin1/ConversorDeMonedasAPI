using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;

namespace ConversorDeMonedasBack.Data.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {   
        private readonly CurrencyConverterContext _context;
        public SubscriptionService(CurrencyConverterContext context)
        {
            _context = context;
        }
        public Subscription GetSubscriptionById(int subscriptionId)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
        }
    }
}
