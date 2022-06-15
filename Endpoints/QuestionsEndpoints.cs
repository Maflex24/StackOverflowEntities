using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StackOverflowEntities.Entities;
using StackOverflowEntities.Entities.Dtos;
using StackOverflowEntities.Migrations;

namespace StackOverflowEntities
{
    public static class QuestionsEndpoints
    {
        public static WebApplication RegisterQuestionsEndpoints(this WebApplication app)
        {
            app.MapGet("questions", async (StackOverflowContext db, int page, int resultsPerPage, string? searchPhrase, string? orderBy, bool? orderByDesc) =>
            {
                var query = db.Questions
                    .AsNoTracking()
                    .Where(q => searchPhrase == null ||
                                (q.Title.ToLower().Contains(searchPhrase.ToLower()) ||
                                 q.Content.ToLower().Contains(searchPhrase.ToLower())));

                var skippedElements = resultsPerPage * (page - 1);
                var totalQuestionsQty = query.Count();

                if (orderBy != null)
                {
                    var columnExpression = new Dictionary<string, Expression<Func<Question, object>>>()
                    {
                        {nameof(Question.Title), q => q.Title},
                        {nameof(Question.Comments), q => q.Comments.Count},
                        {nameof(Question.Created), q => q.Created},
                        {nameof(Question.LastEdited), q => q.LastEdited},
                        {nameof(Question.Rating), q => q.Rating},
                        {nameof(Question.Replies), q => q.Replies.Count},
                        {nameof(Question.Tags), q => q.Tags.Count}
                    };

                    var sortByExpression = columnExpression[orderBy];

                    query = orderByDesc == true
                        ? query.OrderByDescending(sortByExpression)
                        : query.OrderBy(sortByExpression);
                }

                var questions = await query
                    .AsNoTracking()
                    .Skip(skippedElements)
                    .Take(resultsPerPage)
                    .Select(q => new QuestionInfoDto()
                    {
                        Comments = q.Comments.Count, 
                        ContentShort = q.Content.Substring(0, 200), 
                        Id = q.Id, 
                        Rating = q.Rating, 
                        Replies = q.Replies.Count, 
                        Title = q.Title,
                        Tags = q.Tags
                    })
                    .ToListAsync();

                var result = new AllQuestionsPageDto(totalQuestionsQty, page, resultsPerPage, questions);

                return result;

            }).WithTags("Questions");


            app.MapGet("question", async (StackOverflowContext db, Guid questionId) =>
            {
                var result = await db.Questions
                    .AsNoTracking()
                    .Include(q => q.Replies)
                    .Include(q => q.Comments)
                    .Include(q => q.Tags)
                    .Include(q => q.Author)
                    .Select(q  => new SingleQuestionDto()
                    {
                        Id = q.Id,
                        Title = q.Title,
                        Author = new AuthorDto(){Id = q.AuthorId, Name = q.Author.Name},
                        Rating = q.Rating,
                        Created = q.Created,
                        LastUpdate = q.LastEdited,
                        Tags = (List<TagDto>) q.Tags.Select(t => new TagDto(){Id = t.Id, Name = t.Name}),
                        Content = q.Content,
                        Replies = (List<QuestionReplyDto>) q.Replies.Select(r => new QuestionReplyDto()
                        {
                            Id = r.Id,
                            Author = new AuthorDto() { Id = r.AuthorId, Name = r.Author.Name },
                            Rating = r.Rating,
                            Created = r.Created,
                            LastUpdate = r.LastEdited,
                            Content = r.Content
                        }),
                        Comments = (List<CommentDto>) q.Comments.Select(c => new CommentDto()
                        {
                            Id = c.Id,
                            Author = new AuthorDto() { Id = c.AuthorId, Name = c.Author.Name },
                            Rating = c.Rating,
                            Created = c.Created,
                            LastUpdate = c.LastEdited,
                            ReplyId = c.ReplyId,
                            Content = c.Content,
                        })
                    })
                    .FirstAsync(q => q.Id == questionId);

                return result;
            }).WithTags("Questions");


            app.MapPost("question", async (StackOverflowContext db, QuestionPostModel questionPostModel) =>
            {
                var question = new Question();
                question.Title = questionPostModel.Title;
                question.Content = questionPostModel.Content;
                question.AuthorId = questionPostModel.AuthorId;
                question.Created = DateTime.Now;


                await db.Questions.AddAsync(question);
                await db.SaveChangesAsync();

                return question;
            }).WithTags("Questions");


            app.MapPut("question", async (StackOverflowContext db, Guid QuestionId, string? title, string? content) =>
            {
                var question = await db.Questions
                    .FirstAsync(q => q.Id == QuestionId);

                if (title != null) question.Title = title;
                if (content != null) question.Content = content;
                question.LastEdited = DateTime.Now;

                await db.SaveChangesAsync();

                return question;
            }).WithTags("Questions");


            app.MapDelete("question", async (StackOverflowContext db, Guid questionId) =>
            {
                var question = await db.Questions
                    .Where(q => q.Id == questionId)
                    .FirstAsync();

                var replies = await db.Replies
                    .Where(r => r.QuestionId == questionId)
                    .ToListAsync();

                var comments = await db.Comments
                    .Where(c => c.QuestionId == questionId)
                    .ToListAsync();


                db.Replies.RemoveRange(replies);
                db.Comments.RemoveRange(comments);
                db.Questions.Remove(question);
                await db.SaveChangesAsync();

                return question;

            }).WithTags("Questions");

            return app;
        }

    }
}