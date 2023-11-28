using AutoMapper;
using FinalTerm.Common;
using FinalTerm.Dto;
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
    public class OrderController : Controller {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper) {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<Order>>>> GetOrder() {
            return Ok(new ResponseObject<List<Order>>(Ok().StatusCode, "Success", await _orderRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<Order>>> GetOrderById([FromRoute] Guid id) {
            return Ok(new ResponseObject<Order>(Ok().StatusCode, "Success", await _orderRepository.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<Order>>> AddOrder([FromBody] CreateOrderDto rawOrder) {
            return Ok(new ResponseObject<Order>(Ok().StatusCode, "Success", await _orderRepository.AddTransaction(rawOrder)));
        }

        [HttpGet("{phone:regex(^\\d+$)}")]
        public async Task<ActionResult<ResponseObject<List<Order>>>> GetOrderByPhone([FromRoute] string phone) {
            return Ok(new ResponseObject<List<Order>>(Ok().StatusCode, "Success", await _orderRepository.GetOrdersByPhone(phone)));
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<ResponseObject<Order>>> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderDto rawOrder) {
        //    // order in EntityState.Unchanged
        //    Order order = await _orderRepository.GetById(id);

        //    // foundOrder in EntityState.Modified
        //    _mapper.Map(rawOrder, order);
        //    //order.UpdatedAt = DateTime.Now;

        //    return Ok(new ResponseObject<Order>(Ok().StatusCode, "Success", await _orderRepository.Update(order)));
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ResponseObject<Order>>> DeleteOrder([FromRoute] Guid id) {
        //    return Ok(new ResponseObject<Order>(Ok().StatusCode, "Success", await _orderRepository.Delete(id)));
        //}
    }
}
