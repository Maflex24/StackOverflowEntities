using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities
{
    public abstract class QuestionReplyCommentModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastEdited { get; set; }
    }
}
