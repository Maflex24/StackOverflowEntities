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

            builder.HasData(
                new Tag{Id = 1, Name = "C#"},
                new Tag{Id = 2, Name = "Javascript"},
                new Tag{Id = 3, Name = "DependencyInjection"},
                new Tag{Id = 4, Name = ".Net"},
                new Tag{Id = 5, Name = ".NetCore" },
                new Tag{Id = 6, Name = ".Asp.NetCore" },
                new Tag{Id = 7, Name = "WebAPI" },
                new Tag{Id = 8, Name = "EntityFramework" },
                new Tag{Id = 9, Name = "SQL" }
            );
        }
    }
}
