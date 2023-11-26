using Microsoft.AspNetCore.Mvc;
using FinalTerm.Models;
using FinalTerm.Common;
using FinalTerm.Interfaces;
using FinalTerm.Dto;
using AutoMapper;
using FinalTerm.Filters;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [ErrorHandlerFilter]
    public class UserController : Controller {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper) {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<User>>>> GetUser() {
            return Ok(new ResponseObject<List<User>>(Ok().StatusCode, "Success", await _userRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> GetUserById([FromRoute] Guid id) {
            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<User>>> AddUser([FromBody] CreateUserDto rawUser) {
            User user = _mapper.Map<User>(rawUser);

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Add(user)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto rawUser) {
            // user in EntityState.Unchanged
            User user = await _userRepository.GetById(id);

            // foundUser in EntityState.Modified
            _mapper.Map(rawUser, user);
            //user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Update(user)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> DeleteUser([FromRoute] Guid id) {
            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Delete(id)));
        }
    }
}
