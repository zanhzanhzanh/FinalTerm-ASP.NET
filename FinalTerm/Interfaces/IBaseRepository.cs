using FinalTerm.Dto;
using FinalTerm.Models;

namespace FinalTerm.Interfaces {
    public interface IBaseRepository<T> where T : BaseModel {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
        Task<List<T>> GetAllAndPaging(PagingDto pagingDto);
    }
}
