using Lanthanum.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder
                .Property(b => b.DateOfBan)
                .ValueGeneratedOnAdd();
        }
    }
}