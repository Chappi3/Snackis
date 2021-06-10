using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackisAPI.Models;

namespace SnackisAPI.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(r => r.CommentId).IsRequired(false);
            builder.Property(r => r.PostId).IsRequired(false);
            builder.Property(r => r.Content).IsRequired();
            builder.Property(r => r.ByUser).IsRequired();
        }
    }
}
