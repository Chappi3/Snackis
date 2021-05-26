using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackisAPI.Models;

namespace SnackisAPI.Data.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(s => s.Name).IsRequired();

            builder.HasMany(s => s.Posts).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
