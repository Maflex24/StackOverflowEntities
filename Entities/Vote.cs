using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverflowEntities.Entities.Dtos;

namespace StackOverflowEntities.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ElementId { get; set; }
        public int Value { get; set; }
    }

    
}
