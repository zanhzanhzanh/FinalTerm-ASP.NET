using AutoMapper;
using FinalTerm.Common;
using FinalTerm.Common.HandlingException;
using FinalTerm.Dto;
using FinalTerm.Filters;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [ErrorHandlerFilter]
    public class ProductController : Controller {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper) {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<Product>>>> GetProduct() {
            return Ok(new ResponseObject<List<Product>>(Ok().StatusCode, "Success", await _productRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<Product>>> GetProductById([FromRoute] Guid id) {
            return Ok(new ResponseObject<Product>(Ok().StatusCode, "Success", await _productRepository.GetById(id)));
        }

        [HttpGet("{barcode:regex(^\\d+$)}")]
        public async Task<ActionResult<ResponseObject<Product>>> GetProduct(string barcode) {
            return Ok(new ResponseObject<Product>(Ok().StatusCode, "Success",
                await _productRepository.GetByBarcode(barcode) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"Product Not Found")
            ));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<Product>>> AddProduct([FromBody] CreateProductDto rawProduct) {
            if (await _productRepository.GetByBarcode(rawProduct.Barcode) != null) throw new ApiException((int)HttpStatusCode.NotFound, $"Barcode Exist");
            
            Product product = _mapper.Map<Product>(rawProduct);

            return Ok(new ResponseObject<Product>(Ok().StatusCode, "Success", await _productRepository.Add(product)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<Product>>> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto rawProduct) {
            // product in EntityState.Unchanged
            Product product = await _productRepository.GetById(id);

            // Check Barcode Exist
            if (rawProduct.Barcode != null && await _productRepository.GetByBarcode(rawProduct.Barcode) != null)
                throw new ApiException((int)HttpStatusCode.NotFound, $"Barcode Exist");

            // foundProduct in EntityState.Modified
            _mapper.Map(rawProduct, product);
            product.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<Product>(Ok().StatusCode, "Success", await _productRepository.Update(product)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<Product>>> DeleteProduct([FromRoute] Guid id) {
            return Ok(new ResponseObject<Product>(Ok().StatusCode, "Success", await _productRepository.Delete(id)));
        }

        [HttpGet("paging/{PageNumber}/{PageSize}/{SortType}/{SortField}")]
        public async Task<ActionResult<ResponseObject<List<Product>>>> GetProductAndPaging([FromRoute] PagingDto pagingDto) {
            return Ok(new ResponseObject<List<Product>>(Ok().StatusCode, "Success", await _productRepository.GetAllAndPaging(pagingDto)));
        }
    }
}
