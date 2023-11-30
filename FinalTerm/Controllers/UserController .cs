using Microsoft.AspNetCore.Mvc;
using FinalTerm.Models;
using FinalTerm.Common;
using FinalTerm.Interfaces;
using FinalTerm.Dto;
using AutoMapper;
using FinalTerm.Filters;
using Microsoft.AspNetCore.Authorization;
using BC = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;

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

        [HttpPut("avatar")]
        public async Task<ActionResult<ResponseObject<string>>> UpdateAvatarUser([FromHeader(Name = "Authorization")] string token) {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token[7..]);
            string id = jwtSecurityToken.Claims.First(x => x.Type == "Id").Value;

            User user = await _userRepository.GetById(new Guid(id));
            user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<string>(Ok().StatusCode, "Success", await _userRepository.UpdateAvatar(user)));
        }

        [HttpPut]
        public async Task<ActionResult<ResponseObject<User>>> UpdateUser([FromHeader(Name = "Authorization")] string token, [FromBody] UpdateUserDto rawUser) {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token[7..]);
            string id = jwtSecurityToken.Claims.First(x => x.Type == "Id").Value;

            // user in EntityState.Unchanged
            User user = await _userRepository.GetById(new Guid(id));

            if (rawUser.Password != null) { rawUser.Password = BC.HashPassword(rawUser.Password); }

            // foundUser in EntityState.Modified
            _mapper.Map(rawUser, user);
            user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Update(user)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> BlockUser([FromRoute] Guid id, [FromBody] bool isBlocked) {
            // user in EntityState.Unchanged
            User user = await _userRepository.GetById(id);

            // foundUser in EntityState.Modified
            user.IsBlocked = isBlocked;
            user.UpdatedAt = DateTime.Now;

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Update(user)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> DeleteUser([FromRoute] Guid id) {
            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Delete(id)));
        }
    }
}
