using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankSystem.Data
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Position> Positions => Set<Position>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Currency> Currencys => Set<Currency>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assemby = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assemby);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=37.26.136.126;Port=5000;Database=bank_system;Username=postgres;Password=iNYOqU3aq7xEAiScCwYX");
        }
    }
}
