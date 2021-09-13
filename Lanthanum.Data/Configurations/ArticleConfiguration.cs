using Lanthanum.Web.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Web.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasMany(a => a.Authors)
                .WithMany(u => u.PublishedArticles);

            builder
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article);

            builder
                .HasMany(a => a.Teams)
                .WithMany(t => t.Articles);

            builder
                .HasMany(a => a.KindsOfSport)
                .WithMany(k => k.Articles);

            builder
                .Property(a => a.DateTimeOfCreation)
                .ValueGeneratedOnAdd();
        }
    }
}