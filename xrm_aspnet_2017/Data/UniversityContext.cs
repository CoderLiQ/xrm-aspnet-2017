using xrm_aspnet_2017.Models;
using Microsoft.EntityFrameworkCore;

namespace xrm_aspnet_2017.Data {
    public class UniversityContext : DbContext {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(b => b.FirstMidName)
                .IsRequired();
        }
    }
}
