using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<Web.Domain.User>
    {
        public void Configure(EntityTypeBuilder<Web.Domain.User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasOne(u => u.Subscription)
                .HasForeignKey<Subscription>(s => s.OwnerId);

            builder
                .Property(u => u.RegistrationDate)
                .ValueGeneratedOnAdd();
        }
    }
}