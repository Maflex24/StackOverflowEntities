using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class ReplyPostModel
    {
        public Guid QuestionId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
    }
}
