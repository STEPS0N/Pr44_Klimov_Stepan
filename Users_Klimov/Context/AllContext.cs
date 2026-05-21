using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users_Klimov.Models;

namespace Users_Klimov.Context
{
    public class AllContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public AllContext()
        {
            Database.EnsureCreated();
            Users.Load();
            Roles.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=UserRole;uid=root;pwd=;",
                new MySqlServerVersion(new Version(8,0,11)));
        }
    }
}
