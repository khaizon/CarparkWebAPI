using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
        static HttpClient client = new HttpClient();


        public CarparkController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarparks()
        {
            client.BaseAddress = new Uri("https://localhost:5001/api/carpark");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            CarparkData carparkData = null;
            HttpResponseMessage response = await client.GetAsync("https://api.data.gov.sg/v1/transport/carpark-availability");
            if (response.IsSuccessStatusCode)
            {
                carparkData = await response.Content.ReadAsAsync<CarparkData>();
            }
            return Ok(carparkData);
        }
    }
}
