using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcJsTreeTwoEntities2.Models;

namespace MvcJsTreeTwoEntities2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Role { get; set; }
        public DbSet<Legimitation> Legimitation { get; set; }
    }
}
