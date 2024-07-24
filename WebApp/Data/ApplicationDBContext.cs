using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApplicationDBContext(DbContextOptions options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Adventure", Description = "Funny", DisplayPriority = 1 },
                new Category { Id = 2, Name = "Roman", Description = "Romantic", DisplayPriority = 4 },
                new Category { Id = 3, Name = "Horror", Description = "So scary", DisplayPriority = 2 },
                new Category { Id = 4, Name = "Scifi", Description = "You will love it", DisplayPriority = 3 });
        }
    }
}
