using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StackOverflowEntities.Entities
{
    public class StackOverflowContext : DbContext
    {
        public StackOverflowContext(DbContextOptions<StackOverflowContext> options) : base(options)
        {

        }

        public DbSet<QuestionReplyCommentModel> QuestionReplyCommentModels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<DiscriminatorView> DiscriminatorViews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<DiscriminatorView>(e =>
            {
                e.ToView("View_Discriminator");
            });
        }
    }
}
