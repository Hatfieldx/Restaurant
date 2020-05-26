using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess;
using Restaurant.DataAccess.Repository.Interfaces;

namespace Restaurant.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _context.ApplicationUsers });
        }

        [HttpPost("{id}")]
        public IActionResult LockUnlock([FromBody]string id)
        {        
            var item = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return Json(new {success = false, message = "Error while Locking/Unlocking" });
            }

            if (item.LockoutEnd != null && item.LockoutEnd > DateTime.Now)
            {
                item.LockoutEnd = DateTime.Now;
            }
            else
            {
                item.LockoutEnd = DateTime.Now.AddYears(100);      
            }

            _context.SaveChanges();

            return Json(new { success = true, message = "Operation succesful" });
        }
    }
}