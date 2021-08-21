using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Web.Domain.Ban>
    {
        public void Configure(EntityTypeBuilder<Web.Domain.Ban> builder)
        {
            builder
                .Property(b => b.DateOfBan)
                .ValueGeneratedOnAdd();
        }
    }
}