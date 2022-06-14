using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;

namespace StackOverflowEntities.Entities.Dtos
{
    public class SingleQuestionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AuthorDto Author { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Content { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<QuestionReplyDto> Replies { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
