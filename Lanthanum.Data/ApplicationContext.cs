using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); // TODO: change
            AddMockedData(); // TODO: remove
            base.SaveChanges();
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

        private void AddMockedData()
        {
            Users.Add(new User()
            {
                Email = "mail@gmail.com",
                PasswordHash = "12345678"
            });

            Articles.AddRange(new List<Article>()
                
                { 
                    new Article(){

                    LogoPath = "/images/mock_article_img.png",
                    Headline="ArticleHeadlineBlaBlaBla",
                    MainText = "blablablablablablablablablablablablablablabla",
                               Team = new Team()
                    {
                        Name="Foks",
                        Location = "Tennesy",
                        KindOfSport = new KindOfSport()
                        {
                            Name="Football"
                        },
                        Conference = "AFC North"
                        
                    },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Published
                        
                    },

                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="ArticleHeadlineBlaBlaBla",
                        MainText = "dasdasbla",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = new KindOfSport()
                            {
                                Name="Football"
                            },
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished

                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="ArticleHeadlineBlaBlaBla",
                        MainText = "asd",
                        Team = new Team()
                        {
                            Name="Foks",
                            Location = "Tennesy",
                            KindOfSport = new KindOfSport()
                            {
                                Name="Football"
                            },
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished
                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="ArticleHeadlineBlaBlaBla",
                        MainText = "a",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = new KindOfSport()
                            {
                                Name="Football"
                            },
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished

                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="ArticleHeadlineBlaBlaBla",
                        MainText = "b",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = new KindOfSport()
                            {
                                Name="Football"
                            },
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Published

                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="ArticleHeadlineBlaBlaBla",
                        MainText = "f",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = new KindOfSport()
                            {
                                Name="Football"
                            },
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished

                    }
                }
                
            );
        }
    }
}
