using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SnackisAPI.Models;

namespace SnackisAPI.Data
{
    public class SnackisContext : DbContext
    {
        public SnackisContext(DbContextOptions<SnackisContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
