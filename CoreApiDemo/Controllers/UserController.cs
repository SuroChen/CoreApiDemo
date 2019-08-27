using CoreApiDemo.Models;
using CoreApiDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private bool result;
        private readonly UserRepository repo;

        public UserController(YpobDBContent dbContext)
        {
            repo = new UserRepository(dbContext);
        }

        // GET: api/User/getUsers
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<string> getToken()
        {
            return repo.getToken();
        }

        // GET: api/User/getUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> getUsers()
        {
            return await repo.getUsers();
        }

        //GET: api/User/getUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> getUser(int id)
        {
            var User = await repo.getUser(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // POST: api/User/addUser
        [HttpPost]
        public async Task<IActionResult> addUser([FromBody] User user)
        {
            result = await repo.addUser(user);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/User/modifyUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> modifyUser(int id, [FromBody] User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            result = await repo.modifyUser(id, user);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/User/destroyUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> destroyUser(int id)
        {
            result = await repo.destroyUser(id);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
