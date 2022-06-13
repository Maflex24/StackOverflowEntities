using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        public static async void TagsSeed(StackOverflowContext db)
        {
            var tags = new List<Tag>()
            {
                new Tag {Name = "C#"},
                new Tag {Name = "Javascript"},
                new Tag {Name = "DependencyInjection"},
                new Tag {Name = ".Net"},
                new Tag {Name = ".NetCore"},
                new Tag {Name = ".Asp.NetCore"},
                new Tag {Name = "WebAPI"},
                new Tag {Name = "EntityFramework"},
                new Tag {Name = "SQL"}
            };

            await db.Tags.AddRangeAsync(tags);
            await db.SaveChangesAsync();
        }
    }


    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasMany(q => q.Questions)
                .WithMany(t => t.Tags);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
