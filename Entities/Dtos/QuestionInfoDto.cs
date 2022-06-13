using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class QuestionInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ContentShort { get; set; }
        public int Replies { get; set; }
        public int Comments { get; set; }
        public int Rating { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
