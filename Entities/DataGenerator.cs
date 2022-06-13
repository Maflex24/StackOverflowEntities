using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace StackOverflowEntities.Entities
{
    public class DataGenerator
    {
        public static async Task<List<User>> UsersSeed(StackOverflowContext dbContext)
        {
            Randomizer.Seed = new Random(1024);

            var userGenerator = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Internet.UserName());

            var usersCount = 250;
            var users = userGenerator.Generate(usersCount);

            await dbContext.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();

            return users;
        }

        public static async Task<List<Question>> QuestionsSeed(StackOverflowContext dbContext, List<User> users)
        {
            Randomizer.Seed = new Random(1024);
            var tags = await dbContext.Tags
                .ToListAsync();

            var tagMaxIndex = tags.Count - 1;

            var questionGenerator = new Faker<Question>()
                .RuleFor(q => q.Title, f => f.Lorem.Sentence(15).Substring(0, 35) + "...?")
                .RuleFor(q => q.Content, f => f.Lorem.Paragraphs(1, 25, "\n\n"))
                .RuleFor(q => q.Rating, f => f.Random.Number(-5, 15))
                .RuleFor(q => q.Author, f => users[f.Random.Number(users.Count - 1)])
                .RuleFor(q => q.Created, f => f.Date.Past())
                .RuleFor(q => q.Tags, f => f.Random.ListItems(tags));

            var questions = questionGenerator.Generate(500);

            await dbContext.AddRangeAsync(questions);
            await dbContext.SaveChangesAsync();

            return questions;
        }

        public static async Task<List<Reply>> ReplySeed(StackOverflowContext dbContext, List<User> users, List<Question> questions)
        {
            Randomizer.Seed = new Random(1024);

            var replyGenerator = new Faker<Reply>()
                .RuleFor(r => r.Content, f => f.Lorem.Paragraphs(1, 15, "\n\n"))
                .RuleFor(r => r.Rating, f => f.Random.Number(-5, 15))
                .RuleFor(r => r.Author, f => f.Random.ListItem(users))
                .RuleFor(r => r.Question, f => f.Random.ListItem(questions))
                .RuleFor(r => r.Created, DateTime.Now);

            var replies = replyGenerator.Generate(1000);

            await dbContext.AddRangeAsync(replies);
            await dbContext.SaveChangesAsync();

            return replies;
        }


        public static async void CommentsSeed(StackOverflowContext db)
        {
            Randomizer.Seed = new Random(1024);

            var users = await db.Users
                .AsNoTracking()
                .Select(u => u.Id)
                .ToListAsync();

            var questions = await db.Questions
                .AsNoTracking()
                .Select(q => q.Id)
                .ToListAsync();

            var commentsToQuestionsGenerator = new Faker<Comment>()
                .RuleFor(r => r.Content, f => f.Lorem.Sentences())
                .RuleFor(r => r.Rating, f => f.Random.Number(-5, 15))
                .RuleFor(r => r.AuthorId, f => f.Random.ListItem(users))
                .RuleFor(r => r.QuestionId, f => f.Random.ListItem(questions))
                .RuleFor(r => r.Created, DateTime.Now);

            var commentsToQuestions = commentsToQuestionsGenerator.Generate(300);

            var replies = await db.Replies
                .AsNoTracking()
                .Select(r => r.Id)
                .ToListAsync();

            var commentsToRepliesGenerator = new Faker<Comment>()
                .RuleFor(r => r.Content, f => f.Lorem.Sentences())
                .RuleFor(r => r.Rating, f => f.Random.Number(-5, 15))
                .RuleFor(r => r.AuthorId, f => f.Random.ListItem(users))
                .RuleFor(r => r.ReplyId, f => f.Random.ListItem(replies))
                .RuleFor(r => r.Created, DateTime.Now);

            var commentsToReplies = commentsToRepliesGenerator.Generate(1000);

            await db.AddRangeAsync(commentsToReplies);
            await db.AddRangeAsync(commentsToQuestions);
            await db.SaveChangesAsync();
        }
    }
}
