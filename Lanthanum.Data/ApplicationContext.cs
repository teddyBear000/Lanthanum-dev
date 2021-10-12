using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanthanum.Data.Configurations;
using Microsoft.AspNetCore.Http;
using Lanthanum.Web.Data.Domain;

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
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); // TODO: change
            AddMockedData();
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

        private void AddMockedData() //TODO REMOVE LATER
        {

            KindsOfSport.AddRange(
               new List<KindOfSport>()
               {
                    new KindOfSport
                    {
                        Name = "Home"
                    },
                    new KindOfSport()
                    {
                        Name = "NBA"
                    },
                    new KindOfSport()
                    {
                         Name = "NFL"
                    },
                    new KindOfSport()
                    {
                         Name = "MLB"
                    },
                    new KindOfSport()
                    {
                         Name = "CBB"
                    },
                    new KindOfSport()
                    {
                         Name = "NASCAR"
                    },
                    new KindOfSport()
                    {
                         Name = "GOLF"
                    },
                    new KindOfSport()
                    {
                         Name = "SOCCER"
                    },
                    new KindOfSport()
                    {
                         Name = "TEAMHUB"
                    },
                    new KindOfSport()
                    {
                         Name = "LIFESTYLE"
                    },
                    new KindOfSport()
                    {
                         Name = "DEALBOOK"
                    },
                    new KindOfSport
                    {
                        Name = "VIDEO"
                    }
               });

            Teams.Add(new Team
            {
                Name = "Barcelona",
                Location = "Spain"
            });

            Teams.Add(new Team
            {
                Name = "Chelsy",
                Location = "England"
            });

            Conferences.Add(new Conference
            {
                Name = "AFC England",
            });

            Conferences.Add(new Conference
            {
                Name = "EFC Africa",
            });

            Users.Add(new User()
            {
                Email = "mail@gmail.com",
                PasswordHash = "12345678"
            });

            base.SaveChanges();

            Articles.AddRange(new List<Article>()

                {
                    new Article(){
                    Headline="Article1",
                    MainText = "The singer’s new engagement to Asghari is mentioned as one of the chief reasons Britney wants the co.213123123123;",
                    LogoPath = "/images/mock_article_img.png",
                    Team = new Team()
                    {
                        Name="Foks",
                        Location = "Tennesy",
                        KindOfSport = KindsOfSport.Find(1),
                        Conference = "AFC South"
                    },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Published,
                        KindOfSport = KindsOfSport.Find(1),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }
                    },

                    new Article(){
                        Headline="Article2",
                        MainText = "dasdasbla",
                        LogoPath = "/images/mock_article_img.png",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(2),
                            Conference = "AFC North"

                        },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(2),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }
                    },
                    new Article() {
                        Headline="Article3",
                        MainText = "asd",
                        LogoPath = "/images/mock_article_img.png",
                        Team = new Team()
                        {
                            Name="Foks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(2),
                            Conference = "AFC West"

                        },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(2),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }
                    },
                    new Article() {
                        Headline="Article4",
                        MainText = "a",
                        LogoPath = "/images/mock_article_img.png",
                        Team = new Team()
                        {
                            Name="Jerks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(3),
                            Conference = "AFC South"

                        },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(3),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }

                    },
                    new Article(){
                        Headline="Article5",
                        MainText = "b",
                        LogoPath = "/images/mock_article_img.png",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(3),
                            Conference = "AFC North"
                        },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Published,
                        KindOfSport = KindsOfSport.Find(3),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }
                    },
                    new Article(){
                        Headline="Article6",
                        MainText = "f",
                        LogoPath = "/images/mock_article_img.png",
                        Team = new Team()
                        {
                            Name="Goks",
                            Location = "Tennesy",
                            KindOfSport = KindsOfSport.Find(1),
                            Conference = "AFC West"
                        },
                        Alternative = "img",
                        ArticleStatus = ArticleStatus.Unpublished,
                        KindOfSport = KindsOfSport.Find(1),
                        LogoPicture = new Picture()
                        {
                            LogoPath = "/images/mock_article_img.png",
                            Size = "height: 402px; width: 714.667px;",
                            Crop = "",
                            Filter = ""
                        }
                    }
                }

            );
        }
    }
}
