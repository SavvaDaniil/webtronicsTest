using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Entity;

namespace WebtronicsTestWPF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FileVisit> FileVisits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=webtronicsSqlite.db");
        }


    }
}
