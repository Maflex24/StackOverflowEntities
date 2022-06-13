using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Question : QuestionModel
    {
        public List<Tag>? Tags { get; set; } = new List<Tag>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public List<Reply>? Replies { get; set; } = new List<Reply>();
        public string Title { get; set; }
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Title).HasMaxLength(80).IsRequired();
        }
    }
}
