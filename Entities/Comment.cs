using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Comment : QuestionModel
    {
        public Guid? ReplyId { get; set; }
        public Reply? Reply { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }

    public class CommentConfigure : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(r => r.Reply)
                .WithMany(c => c.Comments)
                .HasForeignKey(r => r.Id);

            builder.HasOne(q => q.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
