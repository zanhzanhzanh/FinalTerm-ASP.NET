using FinalTerm.Dto;
using FinalTerm.Models;

namespace FinalTerm.Interfaces {
    public interface IOrderRepository : IBaseRepository<Order> {
        Task<Order> AddTransaction(CreateOrderDto orderDto);
        Task<List<Order>> GetOrdersByPhone(string phone);
    }
}
