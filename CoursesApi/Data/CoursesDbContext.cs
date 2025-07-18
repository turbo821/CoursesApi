using CoursesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Data
{
    public class CoursesDbContext(DbContextOptions<CoursesDbContext> options)
        : DbContext(options)
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
