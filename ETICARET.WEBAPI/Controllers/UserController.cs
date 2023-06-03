using ETICARET.WEBAPI.Identity;
using ETICARET.WEBAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.WEBAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;


        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /* 
         API (APPLICATION PROGRAMING INTERFACE) UYGULAMA PROGRAMLAMA ARAYÜZÜ
         GET / GET {id} => data getirmek
         POST => data yollayıp karşılığında data olarak cevap getirmek.
         PUT {id} => güncelleme
         DELETE => silme
                 
         */

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            if(_userManager.Users == null)
            {
                return NotFound();
            }

            return await _userManager.Users.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            if (_userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]

        public async Task<ActionResult<User>> PostUser(User model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.EmailAddress,
                FullName = model.FullName
            };

            await _userManager.CreateAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id},user);
        }

    }
}
