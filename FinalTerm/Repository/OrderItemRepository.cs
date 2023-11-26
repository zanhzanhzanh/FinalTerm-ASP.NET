using FinalTerm.Interfaces;
using FinalTerm.Models;

namespace FinalTerm.Repository {
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository {
        public OrderItemRepository(DataContext context) : base(context) { }
    }
}
