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
        public virtual Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public virtual List<Comment>? Comments { get; set; } = new List<Comment>();
    }

    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasOne(r => r.Question)
                .WithMany(q => q.Replies)
                .HasForeignKey(r => r.QuestionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
