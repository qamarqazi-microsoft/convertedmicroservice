using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(Guid id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Guid id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _repository.CreateAsync(order);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            return await _repository.UpdateAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

