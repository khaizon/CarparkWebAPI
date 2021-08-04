using System;
using CarparkAPIApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarparkAPIApp.Data
{
    public class ApiDbContext : IdentityDbContext<UserModel>
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }
    }
}
