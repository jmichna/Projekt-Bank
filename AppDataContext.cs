using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Projekt
{
    public partial class AppDataContext : DbContext
    {
        public AppDataContext()
        {
        }

        public DbSet<Balance> Balance { get; set; }
        public DbSet<Data> Data { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<LoginData> LoginData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = AppData;");
            }
        }
    }
}
