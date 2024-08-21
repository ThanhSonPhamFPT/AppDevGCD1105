using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDBContext:IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public ApplicationDBContext(DbContextOptions options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Adventure", Description = "Funny", DisplayPriority = 1 },
                new Category { Id = 2, Name = "Roman", Description = "Romantic", DisplayPriority = 4 },
                new Category { Id = 3, Name = "Horror", Description = "So scary", DisplayPriority = 2 },
                new Category { Id = 4, Name = "Scifi", Description = "You will love it", DisplayPriority = 3 });
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Programming", Description = "Basic guidance", Author = "Greenwich", Price = 100, CategoryId = 1 },
				new Book { Id = 2, Title = "Data Structures", Description = "Fundamental course for IT", Author = "FPT", Price = 100, CategoryId = 1 },
				new Book { Id = 3, Title = "App Dev", Description = "Advanced application guidance", Author = "Greenwich", Price = 100, CategoryId = 1 },
				new Book { Id = 4, Title = "One Piece", Description = "Good anime", Author = "Some one", Price = 100, CategoryId = 2 },
				new Book { Id = 5, Title = "Robinhood", Description = "adventure, so fun", Author = "Best Author", Price = 100, CategoryId = 3 });
        }
    }
}
