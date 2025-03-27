using GraphQLPractive.Model;
using Microsoft.EntityFrameworkCore;
using StudentManage.Data.Configurations;

namespace StudentManage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /****
            modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
            modelBuilder.Entity<Student>()
            .Property(s => s.Class)
            .HasMaxLength(50);
            ****/
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}
