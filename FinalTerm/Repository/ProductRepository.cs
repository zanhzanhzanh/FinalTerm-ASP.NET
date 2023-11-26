using FinalTerm.Interfaces;
using FinalTerm.Models;

namespace FinalTerm.Repository {
    public class ProductRepository : BaseRepository<Product>, IProductRepository {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context) {
            this._context = context;
        }

        public async Task<Product?> GetByBarcode(string barcode) {
            Product? foundEntity = await _context.Products.Where(i => i.Barcode == barcode).FirstOrDefaultAsync();

            return foundEntity;
        }
    }
}
