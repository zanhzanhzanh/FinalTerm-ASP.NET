using Microsoft.AspNetCore.Mvc;
using FinalTerm.Models;
using FinalTerm.Common;
using FinalTerm.Interfaces;
using FinalTerm.Dto;
using AutoMapper;
using FinalTerm.Filters;
using Microsoft.AspNetCore.Authorization;
using BC = BCrypt.Net.BCrypt;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
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

        [HttpPut("avatar/{id}")]
        public async Task<ActionResult<ResponseObject<User>>> UpdateAvatarUser([FromRoute] Guid id) {
            User user = await _userRepository.GetById(id);
            user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.UpdateAvatar(user)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto rawUser) {
            // user in EntityState.Unchanged
            User user = await _userRepository.GetById(id);

            if (rawUser.Password != null) { rawUser.Password = BC.HashPassword(rawUser.Password); }

            // foundUser in EntityState.Modified
            _mapper.Map(rawUser, user);
            user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Update(user)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> DeleteUser([FromRoute] Guid id) {
            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Delete(id)));
        }
    }
}
