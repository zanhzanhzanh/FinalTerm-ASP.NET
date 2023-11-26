using FinalTerm.Models;

namespace FinalTerm.Interfaces {
    public interface ICustomerRepository : IBaseRepository<Customer> {
        Task<Customer?> GetByPhone(string phone);
    }
}
