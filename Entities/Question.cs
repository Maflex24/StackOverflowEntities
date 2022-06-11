using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowEntities.Entities
{
    public class Question : QuestionReplyCommentModel
    {
        public List<Tag>? Tags { get; set; } = new List<Tag>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public List<Reply>? Replies { get; set; } = new List<Reply>();

        public void Configure(EntityTypeBuilder<Question> builder)
        {
            
        }
    }
}
