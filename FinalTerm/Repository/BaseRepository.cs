using FinalTerm.Common.HandlingException;
using FinalTerm.Dto;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using System.Linq.Expressions;
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

        public virtual async Task<List<T>> GetAllAndPaging(PagingDto pagingDto) {
            IQueryable<T> query = _context.Set<T>();
            
            if (!string.IsNullOrWhiteSpace(pagingDto.SortField)) {
                if(pagingDto.SortField == "default") {
                    query = pagingDto.SortType == true ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                } else {
                    // Take T Properties
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, pagingDto.SortField);
                    var lambda = Expression.Lambda(property, parameter);

                    // Get Method OrderBy
                    string methodName = pagingDto.SortType == true ? "OrderBy" : "OrderByDescending";
                    var methodCall = Expression.Call(
                        typeof(Queryable),methodName,
                        new[] { typeof(T), property.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    );

                    // Create Query
                    query = query.Provider.CreateQuery<T>(methodCall);
                }
            }

            List<T> result = await query
                .Skip((pagingDto.PageNumber - 1) * pagingDto.PageSize)
                .Take(pagingDto.PageSize)
                .ToListAsync();

            return result;
        }
    }
}
