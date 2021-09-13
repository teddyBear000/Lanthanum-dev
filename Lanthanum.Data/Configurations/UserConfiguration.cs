using Lanthanum.Web.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Web.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasOne(u => u.Subscription)
                .WithOne(s => s.Owner)
                .HasForeignKey<Subscription>(s => s.OwnerId);

            builder
                .Property(u => u.RegistrationDate)
                .ValueGeneratedOnAdd();
        }
    }
}