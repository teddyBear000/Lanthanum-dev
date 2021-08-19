using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lanthanum.Data.Configurations
{
    public class CommentConfiguration: IEntityTypeConfiguration<Web.Domain.Comment>
    {
        public void Configure(EntityTypeBuilder<Web.Domain.Comment> builder)
        {
            builder
                .Property(c => c.DateTimeOfCreation)
                .ValueGeneratedOnAdd();
        }
    }
}