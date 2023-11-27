using FinalTerm.Models;

namespace FinalTerm.Interfaces {
    public interface IUserRepository : IBaseRepository<User> {
        Task<User> GetByEmail(string email);
    }
}
