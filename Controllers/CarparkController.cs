using System;
using System.Threading.Tasks;
using CarparkAPIApp.Data;
using CarparkAPIApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarparkAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarparkController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CarparkController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarparks()
        {
            var carparks = await _context.Carparks.ToListAsync();

            return Ok(carparks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarpark(CarparkData data)
        {
            if (ModelState.IsValid)
            {
                await _context.Carparks.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCarpark", new { data.ID }, data);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarpark(int id)
        {
            var carpark = await _context.Carparks.FirstOrDefaultAsync(x => x.ID == id);

            if(carpark ==null)
                return NotFound();

            return Ok(carpark);
        }
    }
}
