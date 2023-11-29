using FinalTerm.Common.HandlingException;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;
using BC = BCrypt.Net.BCrypt;

namespace FinalTerm.Repository {
    public class UserRepository : BaseRepository<User>, IUserRepository {
        private readonly BucketsRepository _bucketRepository;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public UserRepository(DataContext context, IConfiguration configuration, BucketsRepository bucketsRepository) : base(context) {
            this._bucketRepository = bucketsRepository;
            this._configuration = configuration;
            this._context = context;
        }

        public override async Task<User> Add(User user) {
            if (await _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync() != null)
                throw new ApiException((int)HttpStatusCode.BadRequest, "Email Exist");

            user.Username = user.Email.Split("@")[0];
            user.Password = BC.HashPassword("1");
            user.Avatar = "https://s3.ap-southeast-1.amazonaws.com/finalterm-asp.net-bucket/defaultAvatar.png";

            return await base.Add(user);
        }

        public override async Task<User> Delete(Guid id) {
            User foundEntity = await _context.Users.FindAsync(id) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"User Not Found");

            string[] getFileName = foundEntity.Avatar.Split("/");
            // Delete Image from S3
            _bucketRepository.DeleteFileAsync(getFileName[getFileName.Length - 1]);

            _context.Users.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }

        public async Task<string> UpdateAvatar(User entity) {
            entity.Avatar = "https://s3." +
                            _configuration.GetSection("AWS:Region").Value +
                            ".amazonaws.com/" + _configuration.GetSection("AWS:BucketName").Value +
                            "/" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss-fffffff") + "_avatar.png";
            return (await Update(entity)).Avatar;
        }

        public async Task<User> GetByEmail(string email) {
            User foundEntity = await _context.Users.Where(i => i.Email == email).FirstOrDefaultAsync() 
                ?? throw new ApiException((int)HttpStatusCode.NotFound, "Not Found User"); ;

            return foundEntity;
        }
    }
}
