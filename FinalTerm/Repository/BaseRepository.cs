using FinalTerm.Common.HandlingException;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Net;

namespace FinalTerm.Repository {
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel {
        private readonly DataContext _context;

        public BaseRepository(DataContext context) {
            this._context = context;
        }

        public virtual async Task<List<T>> GetAll() {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id) {
            T foundEntity = await _context.Set<T>().FindAsync(id) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"{typeof(T).Name} Not Found");

            return foundEntity;
        }

        public virtual async Task<T> Add(T entity) {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> Delete(Guid id) {
            T foundEntity = await _context.Set<T>().FindAsync(id) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"{typeof(T).Name} Not Found");

            _context.Set<T>().Remove(foundEntity);
            await _context.SaveChangesAsync();
            return foundEntity;
        }
    }
}
