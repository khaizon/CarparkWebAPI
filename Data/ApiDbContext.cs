using System;
using CarparkAPIApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarparkAPIApp.Data
{
    public class ApiDbContext : IdentityDbContext<UserModel>
    {
        public virtual DbSet<CarparkData> Carparks {get; set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }
    }
}
