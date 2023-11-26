using FinalTerm.Models;

namespace FinalTerm.Interfaces {
    public interface IProductRepository : IBaseRepository<Product> {
        Task<Product?> GetByBarcode(string barcode);
    }
}
