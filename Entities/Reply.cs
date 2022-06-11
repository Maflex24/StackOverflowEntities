using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Reply : QuestionReplyCommentModel
    {
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }

    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasOne(q => q.Question)
                .WithMany(q => q.Replies)
                .HasForeignKey(k => k.QuestionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
