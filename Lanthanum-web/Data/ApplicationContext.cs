using Lanthanum_web.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lanthanum_web.Data
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

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
                optionsBuilder.UseMySql("server=localhost;user=root;password=12345678;database=sportschema",
                new MySqlServerVersion(new Version(8, 0, 26)));
        }
    }
}
