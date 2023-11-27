using FinalTerm.Common.HandlingException;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;
using BC = BCrypt.Net.BCrypt;

namespace FinalTerm.Repository {
    public class UserRepository : BaseRepository<User>, IUserRepository {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context) {
            this._context = context;
        }

        public override async Task<User> Add(User user) {
            if (await _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync() != null)
                throw new ApiException((int)HttpStatusCode.BadRequest, "Email Exist");

            user.Username = user.Email.Split("@")[0];
            user.Password = BC.HashPassword("1");

            return await base.Add(user);
        }

        public async Task<User> GetByEmail(string email) {
            User foundEntity = await _context.Users.Where(i => i.Email == email).FirstOrDefaultAsync() 
                ?? throw new ApiException((int)HttpStatusCode.NotFound, "Not Found User"); ;

            return foundEntity;
        }
    }
}
