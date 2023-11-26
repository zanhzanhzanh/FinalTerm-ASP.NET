using FinalTerm.Interfaces;
using FinalTerm.Models;

namespace FinalTerm.Repository {
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context) : base(context) {
            this._context = context;
        }

        public async Task<Customer?> GetByPhone(string phone) {
            Customer? foundEntity = await _context.Customers
                 .Include(i => i.Orders)
                 .ThenInclude(i => i.OrderItems)
                 .ThenInclude(i => i.Product)
                 .Where(i => i.Phone == phone)
                 .FirstOrDefaultAsync();

            return foundEntity;
        }
    }
}
