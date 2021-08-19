using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Web.Domain.Article>
    {
        public void Configure(EntityTypeBuilder<Web.Domain.Article> builder)
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
                .HasMany(a => a.KindsOfSports)
                .WithMany(k => k.Articles);

            builder
                .Property(a => a.DateTimeOfCreation)
                .ValueGeneratedOnAdd();
        }
    }
}