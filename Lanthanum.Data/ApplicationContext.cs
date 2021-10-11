using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanthanum.Data.Configurations;
using Microsoft.AspNetCore.Http;

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
        public DbSet<FooterItem> FooterItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated(); // TODO: change
            //AddMockedData();
            //base.SaveChanges();
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

        private void AddMockedData() //TODO REMOVE LATER
        {
            Users.Add(new User()
            {
                Email = "mail@gmail.com",
                PasswordHash = "12345678"
            });

            KindsOfSport.AddRange(
                new List<KindOfSport>()
                {
                    new KindOfSport()
                    {
                        Name = "Football"
                    },
                    new KindOfSport()
                    {
                        Name = "Basketball"
                    },
                    new KindOfSport()
                    {
                        Name = "Golf"
                    },
                });

            base.SaveChanges();
            Articles.AddRange(new List<Article>()

                {
                    new Article(){

                    LogoPath = "/images/mock_article_img.png",
                    Headline="Article1",
                    MainText = "The singer’s new engagement to Asghari is mentioned as one of the chief reasons Britney wants the co.213123123123;",
                               Team = new Team()
                    {
                        Name="Foks",
                        Location = "Tennesy",
                        KindOfSport = KindsOfSport.Find(1),
                        Conference = "AFC South"
                    },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Published,
                        KindOfSport = KindsOfSport.Find(1)
                    },

                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="Article2",
                        MainText = "dasdasbla",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(2),
                            Conference = "AFC North"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(2)
                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="Article3",
                        MainText = "asd",
                        Team = new Team()
                        {
                            Name="Foks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(2),
                            Conference = "AFC West"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(2)
                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="Article4",
                        MainText = "a",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(3),
                            Conference = "AFC South"

                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(3)

                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="Article5",
                        MainText = "b",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(3),
                            Conference = "AFC North"
                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Published,
                        KindOfSport = KindsOfSport.Find(3)
                    },
                    new Article(){

                        LogoPath = "/images/mock_article_img.png",
                        Headline="Article6",
                        MainText = "f",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(1),
                            Conference = "AFC West"
                        },
                        Alt = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(1)
                    }
                }

            );
        }
    }
}
