using System;
using Microsoft.AspNetCore.Mvc;

namespace CarparkAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarparkController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestRun()
        {
            return Ok("Success");
        }
    }
}
