using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using TaskAPI.Models.DTOs;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DatabaseContext _dbContext;
        public UserController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET : /api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            if(_dbContext.Users == null)
            {
                return NotFound();
            }

            var users = await _dbContext.Users.ToListAsync();
            if(users == null)
            {
                return NotFound("No user was found");
            }

            return Ok(users);
        }

        // GET : /api/User/:id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if(_dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(user);
        }

        // POST : /api/User
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserRequestDto data)
        {
            if(_dbContext.Users == null)
            {
                return NotFound();
            }

            var user = new User
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                MiddleName = data.MiddleName,
                UserName = data.UserName,
                Password = data.Password
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            if (user == null)
            {
                return NotFound();
            }

            var res = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                UserName = user.UserName,
                LastName= user.LastName,
            };

            return Ok(res);
        }

        // PUT : /api/User/:id
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UserRequestDto data)
        {
            if(_dbContext.Users == null)
            {
                return NotFound() ;
            }

            var existingUser = await _dbContext.Users.FindAsync(id);

            if(existingUser == null)
            {
                return NotFound("User does not exist");
            }

            existingUser.FirstName = data.FirstName ?? existingUser.FirstName;
            existingUser.UserName = data.UserName ?? existingUser.UserName;
            existingUser.LastName = data.LastName ?? existingUser.LastName;
            existingUser.MiddleName = data.MiddleName ?? existingUser.MiddleName;
            existingUser.Password = data.Password ?? existingUser.Password;

            await _dbContext.SaveChangesAsync();

            var res = new UserResponseDto
            {
                Id = id,
                FirstName = existingUser.FirstName,
                UserName = existingUser.UserName,
                LastName = existingUser.LastName,
            };

            return Ok(res);

        }

        // DELETE : /api/User/:id
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveUser(int id)
        {
            if( _dbContext.Users == null)
            {
                return NotFound();
            }

            var existingUser = await _dbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound("User does not exist");
            }
            _dbContext.Users.Remove(existingUser);
            return NoContent();
        }
    }
}
