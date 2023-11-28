using FinalTerm.Common.HandlingException;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;

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

        public override async Task<Customer> GetById(Guid id) {
            return await _context.Customers.Include(i => i.Orders).ThenInclude(i => i.OrderItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ApiException((int)HttpStatusCode.NotFound, "Customer Not Found");
        }
    }
}
