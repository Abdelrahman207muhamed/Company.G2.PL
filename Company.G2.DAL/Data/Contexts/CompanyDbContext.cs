using Company.G2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.G2.DAL.Data.Contexts
{
    public class CompanyDbContext :DbContext
    {
        public CompanyDbContext() :base()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = CompanyG2 ; Trusted_Connection = True; TrustServerCertificate = true");
        }
        public DbSet<Department> Departments { get; set; }
    }
}
