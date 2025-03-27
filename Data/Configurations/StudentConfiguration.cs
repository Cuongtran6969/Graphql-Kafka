using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GraphQLPractive.Model;

namespace StudentManage.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
            builder.Property(s => s.Class)
            .HasMaxLength(50);
        }
    }
}
