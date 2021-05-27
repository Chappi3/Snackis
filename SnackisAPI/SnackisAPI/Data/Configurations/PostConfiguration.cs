using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackisAPI.Models;

namespace SnackisAPI.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Content).IsRequired();

            builder.HasOne<SubCategory>().WithMany().HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
