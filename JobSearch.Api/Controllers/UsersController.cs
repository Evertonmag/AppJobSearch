using JobSearch.Api.Data;
using JobSearch.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JobSearchContext _data;

        public UsersController(JobSearchContext data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserAsync(string email, string password)
        {
            var userDb = await _data.Users.FirstOrDefaultAsync(lbda => lbda.Email == email && lbda.Password == password);

            if (userDb == null) return NotFound();

            return Ok(userDb);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            //TODO - Validações

            await _data.Users.AddAsync(user);
            await _data.SaveChangesAsync();

            return Ok(user);
            //return CreatedAtAction(nameof(GetUserAsync), new {email = user.Email, password = user.Password}, user);
        }

    }
}
