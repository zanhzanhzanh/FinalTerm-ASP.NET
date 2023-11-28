using AutoMapper;
using FinalTerm.Common;
using FinalTerm.Dto;
using FinalTerm.Filters;
using FinalTerm.Interfaces;
using FinalTerm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [ErrorHandlerFilter]
    public class AuthController : Controller {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IUserRepository userService, IMapper mapper) {
            _configuration = configuration;
            _userRepository = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<ResponseObject<User>>> Register([FromBody] CreateUserDto rawUser) {
            User user = _mapper.Map<User>(rawUser);

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.Add(user)));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto rawUser) {
            User user = await _userRepository.GetByEmail(rawUser.Email);

            if (!BC.Verify(rawUser.Password, user.Password)) return BadRequest(new ResponseObject<string>(BadRequest().StatusCode, "Wrong Password", ""));

            string token = CreateToken(user);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

            return Ok(new ResponseObject<string>(Ok().StatusCode, "Success", token));
        }

        [Authorize]
        [HttpGet("{token}")]
        public async Task<ActionResult<ResponseObject<User>>> GetUserByToken([FromRoute] string token) {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string id = jwtSecurityToken.Claims.First(x => x.Type == "Id").Value;
            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", await _userRepository.GetById(new Guid(id))));
        }

        private string CreateToken(User user) {
            List<Claim> claims = new() {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Id", user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
