using Lanthanum.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class ActionRequestConfiguration: IEntityTypeConfiguration<ActionRequest>
    {
        public void Configure(EntityTypeBuilder<ActionRequest> builder)
        {
            builder
                .Property(a => a.DateTimeOfCreation)
                .ValueGeneratedOnAdd();
        }
    }
}