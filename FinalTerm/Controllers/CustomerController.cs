using FinalTerm.Common;
using FinalTerm.Common.HandlingException;
using FinalTerm.Filters;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using FinalTerm.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    [ErrorHandlerFilter]
    public class CustomerController : Controller {
        private readonly ICustomerRepository _customerRepository;
        private readonly DataContext _context;

        public CustomerController(DataContext context, ICustomerRepository customerRepository) {
            this._customerRepository = customerRepository;
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<Customer>>>> GetCustomer() {
            return Ok(new ResponseObject<List<Customer>>(Ok().StatusCode, "Success", await _context.Customers
                .Include(i => i.Orders)
                .ThenInclude(i => i.OrderItems)
                .ThenInclude(i => i.Product)
                .ToListAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<Customer>>> GetCustomerById([FromRoute] Guid id) {
            return Ok(new ResponseObject<Customer>(Ok().StatusCode, "Success", await _customerRepository.GetById(id)));
        }

        [HttpGet("{phone:regex(^\\d+$)}")]
        public async Task<ActionResult<ResponseObject<Customer>>> GetCustomer(string phone) {
            return Ok(new ResponseObject<Customer>(Ok().StatusCode, "Success", 
                await _customerRepository.GetByPhone(phone) ?? throw new ApiException((int)HttpStatusCode.NotFound, $"Customer Not Found")
            ));
        }

        //[HttpPost]
        //public async Task<ActionResult<ResponseObject<Customer>>> AddCustomer([FromBody] Customer customer) {
        //    _context.Customers.Add(customer);
        //    await _context.SaveChangesAsync();

        //    return Ok(new ResponseObject<Customer>(Ok().StatusCode, "Success", customer));
        //}
    }
}
