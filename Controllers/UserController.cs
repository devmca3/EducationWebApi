using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EducationWebApi.Models;

namespace EducationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly db_Context _context;

        public UserController(db_Context context)
        {
            _context = context;
        }

        // GET: api/User
        //[HttpGet]
        // public async Task<ActionResult<IEnumerable<UserMaster>>> GetUserMasters()
        // {
        //   if (_context.UserMasters == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _context.UserMasters.ToListAsync();
        // }

        // GET: api/User/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<UserMaster>> GetUserMaster(long id)
        // {
        //   if (_context.UserMasters == null)
        //   {
        //       return NotFound();
        //   }
        //     var userMaster = await _context.UserMasters.FindAsync(id);

        //     if (userMaster == null)
        //     {
        //         return NotFound();
        //     }

        //     return userMaster;
        // }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUserMaster(long id, UserMaster userMaster)
        // {
        //     if (id != userMaster.Userid)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(userMaster).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserMasterExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<UserMaster>> PostUserMaster(UserMaster userMaster)
        // {
        //   if (_context.UserMasters == null)
        //   {
        //       return Problem("Entity set 'db_Context.UserMasters'  is null.");
        //   }
        //     //_context.UserMasters.Add(userMaster);
        //     //await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetUserMaster", new { id = userMaster.Userid }, userMaster);
        // }

        // DELETE: api/User/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUserMaster(long id)
        // {
        //     if (_context.UserMasters == null)
        //     {
        //         return NotFound();
        //     }
        //     var userMaster = await _context.UserMasters.FindAsync(id);
        //     if (userMaster == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.UserMasters.Remove(userMaster);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool UserMasterExists(long id)
        // {
        //     return (_context.UserMasters?.Any(e => e.Userid == id)).GetValueOrDefault();
        // }
    }
}
