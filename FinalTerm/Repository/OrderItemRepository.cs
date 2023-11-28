using FinalTerm.Common.HandlingException;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;

namespace FinalTerm.Repository {
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context) : base(context) {
            this._context = context; 
        }

        public override async Task<OrderItem> GetById(Guid id) {
            return await _context.OrderItems.Include(i => i.Product).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ApiException((int)HttpStatusCode.NotFound, "OrderItem Not Found");
        }
    }
}
