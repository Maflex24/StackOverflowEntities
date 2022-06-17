using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class UsersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Questions { get; set; }
        public int Replies { get; set; }
        public int Comments { get; set; }
    }
}
