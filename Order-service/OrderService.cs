namespace Order
{
    public interface IOrderService
    {
        // Placeholder for future service methods.
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Placeholder for future business logic.
    }
}

