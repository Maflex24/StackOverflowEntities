using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public AuthorDto Author { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Content { get; set; }
    }
}
