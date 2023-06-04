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

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(string id, User model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.Users.Where(i => i.Id == id).FirstOrDefaultAsync();


            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.EmailAddress;

            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }

                throw;

            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (_userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _userManager.DeleteAsync(user);

            return Ok();
        }

        private bool UserExists(string id)
        {
            return (_userManager.Users?.Any(u => u.Id == id)).GetValueOrDefault();
        }

    }
}
