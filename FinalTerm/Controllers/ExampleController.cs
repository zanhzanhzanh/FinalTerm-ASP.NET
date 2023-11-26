using Microsoft.AspNetCore.Mvc;
using FinalTerm.Models;
using FinalTerm.Common;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : Controller {
        private readonly DataContext _context;

        public ExampleController(DataContext context) {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<User>>>> GetUser() {
            return Ok(new ResponseObject<List<User>>(Ok().StatusCode, "Success", await _context.Users.ToListAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> GetUserById([FromRoute] long id) {
            User? foundUser = await _context.Users.FindAsync(id);

            if (foundUser == null) { return NotFound(); }

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", foundUser));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<User>>> AddUser([FromBody] User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> UpdateUser([FromRoute] long id, [FromBody] User user) {
            // foundUser in EntityState.Unchanged
            User? foundUser = await _context.Users.FindAsync(id);

            if (foundUser == null) { return NotFound(); }

            //if (id != user.Id) {
            //    return BadRequest();
            //}
            //_context.Entry(user).State = EntityState.Modified;

            // foundUser in EntityState.Modified, remember to use AutoMapper, UserDTO
            foundUser.Fullname = user.Fullname;
            foundUser.Username = user.Username;
            foundUser.Email = user.Email;
            foundUser.Avatar = user.Avatar;
            foundUser.IsBlocked = user.IsBlocked;

            await _context.SaveChangesAsync();

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", foundUser));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<User>>> DeleteUser([FromRoute] long id) {
            User? foundUser = await _context.Users.FindAsync(id);

            if (foundUser == null) { return NotFound(); }

            _context.Users.Remove(foundUser);
            await _context.SaveChangesAsync();

            return Ok(new ResponseObject<User>(Ok().StatusCode, "Success", foundUser));
        }
    }
}
