using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplicationFitness.Models;

namespace WebApplicationFitness.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Trainer> Trainers { get; set; }
    }
}
