using Lanthanum.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class SubscriptionConfiguration: IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder
                .HasMany(s => s.SubscribedAuthors)
                .WithMany(a => a.Subscribers);

            builder
                .HasMany(s => s.SubscribedSports)
                .WithMany(k => k.Subscribers);

            builder
                .HasMany(s => s.SubscribedTeams)
                .WithMany(t => t.Subscribers);
        }
    }
}