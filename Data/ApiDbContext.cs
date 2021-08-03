using System;
using CarparkAPIApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarparkAPIApp.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<CarparkData> Carparks {get; set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }
    }
}
