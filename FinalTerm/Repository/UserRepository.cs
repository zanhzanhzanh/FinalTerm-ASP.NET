using FinalTerm.Interfaces;
using FinalTerm.Models;

namespace FinalTerm.Repository {
    public class UserRepository : BaseRepository<User>, IUserRepository {
        public UserRepository(DataContext context) : base(context) { }

        //public async Task<List<User>> GetUser() {
        //    return await _context.Users.ToListAsync();
        //}

        //public async Task<User> GetUserById(long id) {
        //    User? foundUser = await _context.Users.FindAsync(id);

        //    return foundUser ?? throw new ApiException((int)HttpStatusCode.NotFound, $"{ this.GetType().Name } Not Found");
        //}

        //public async Task<User> AddUser(User user) {
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //    return user;
        //}

        //public async Task<User> UpdateUser(User user) {
        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return user;
        //}

        //public async Task<User> DeleteUser(long id) {
        //    User foundUser = await _context.Users.FindAsync(id) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"{ this.GetType().Name } Not Found");

        //    _context.Users.Remove(foundUser);
        //    await _context.SaveChangesAsync();

        //    return foundUser;
        //}
    }
}
