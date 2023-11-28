using FinalTerm.Common;
using FinalTerm.Filters;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    [ErrorHandlerFilter]
    public class OrderItemController : Controller {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository) {
            this._orderItemRepository = orderItemRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<OrderItem>>> GetOrderItemById([FromRoute] Guid id) {
            return Ok(new ResponseObject<OrderItem>(Ok().StatusCode, "Success", await _orderItemRepository.GetById(id)));
        }
    }
}
