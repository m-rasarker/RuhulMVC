using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RuhulMVC.Models;

namespace RuhulMVC.Data
{
    public class CliPolmanDbContext : DbContext
    {

        public CliPolmanDbContext(DbContextOptions<CliPolmanDbContext> options) : base(options)
        {
        }



        public DbSet<UserRegistration> UserRegistrations { get; set; }
    }
}
