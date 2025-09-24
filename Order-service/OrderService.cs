using System.Threading.Tasks;

namespace Order
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Placeholder for future business logic
    }
}

