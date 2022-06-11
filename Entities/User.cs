using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Question>? Questions { get; set; } = new List<Question>();
        public List<Reply>? Replies { get; set; } = new List<Reply>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasMany(c => c.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            builder.HasMany(c => c.Replies)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            builder.HasMany(c => c.Questions)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
