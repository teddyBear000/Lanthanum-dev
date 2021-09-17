using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Lanthanum.Data.Configurations;

namespace Lanthanum.Web.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<KindOfSport> KindsOfSport { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<FooterTabItem> FooterTabItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ArticleConfiguration());

            builder
                .ApplyConfiguration(new BanConfiguration());

            builder
                .ApplyConfiguration(new SubscriptionConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());

            builder
                .ApplyConfiguration(new CommentConfiguration());
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
