using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealWorldApp.Core.Tags;

namespace Helper.Infrastructure.DAL.Configurations
{
    internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Name);
            builder.Property(x => x.Count)
                .IsRequired();
        }
    }
}
