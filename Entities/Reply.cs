using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities
{
    public class Reply : QuestionModel
    {
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
