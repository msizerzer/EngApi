using EngApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EngApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.ToListAsync();

            return new JsonResult(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);

            return new JsonResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new JsonResult(user.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            existingUser.Name = user.Name;
            existingUser.BirthDate = user.BirthDate;
            existingUser.IsActive = user.IsActive;
            var success = (await _context.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            _context.Remove(user);
            var success = (await _context.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
