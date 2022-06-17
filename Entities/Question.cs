using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Question : QuestionReplyCommentModel
    {
        public string Title { get; set; }
        public virtual List<Tag>? Tags { get; set; } = new List<Tag>();
        public virtual List<Comment>? Comments { get; set; } = new List<Comment>();
        public virtual List<Reply>? Replies { get; set; } = new List<Reply>();
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Title).HasMaxLength(80).IsRequired();
        }
    }
}
