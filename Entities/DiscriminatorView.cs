using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities
{
    public class DiscriminatorView
    {
        public Guid Id { get; set; }
        public string Discriminator { get; set; }
    }
}
