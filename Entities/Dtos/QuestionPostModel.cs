using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class QuestionPostModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        //public List<Tag>? Tags { get; set; } = new List<Tag>(); // todo implement tag adding
    }
}
