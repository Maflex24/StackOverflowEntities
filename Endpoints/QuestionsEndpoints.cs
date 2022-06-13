using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowEntities.Entities;
using StackOverflowEntities.Entities.Dtos;

namespace StackOverflowEntities
{
    public static class QuestionsEndpoints
    {
        public static WebApplication RegisterQuestionsEndpoints(this WebApplication app)
        {
            app.MapGet("questions", async (StackOverflowContext db, int page, int resultsPerPage) =>
            {
                var totalQuestionsQty = db.Questions
                    .AsNoTracking()
                    .Count();
                var skippedElements = resultsPerPage * (page - 1);

                var questions = await db.Questions
                    .AsNoTracking()
                    .Skip(skippedElements)
                    .Take(resultsPerPage)
                    .Select(q => new QuestionInfoDto(){Comments = q.Comments.Count, ContentShort = q.Content.Substring(0, 200), Id = q.Id, Rating = q.Rating, Replies = q.Replies.Count, Title = q.Title})
                    .ToListAsync();

                var result = new AllQuestionsPageDto(totalQuestionsQty, page, resultsPerPage, questions);

                return result;

            }).WithTags("Questions");


            app.MapGet("question", async (StackOverflowContext db, [FromQuery] Guid questionId) =>
            {
                var result = await db.Questions
                    .AsNoTracking()
                    .Include(q => q.Replies)
                    .Include(q => q.Comments)
                    .Include(q => q.Tags)
                    .Select(q => new { Id = q.Id, Title = q.Title, Author = q.Author.Name, Date = q.Created.ToString("dd-MM-yyyy"), Rating = q.Rating, Tags = q.Tags, Question = q.Content, Replies = q.Replies, Comments = q.Comments })
                    .FirstAsync(q => q.Id == questionId);

                return result;
            }).WithTags("Questions");

            return app;
        }

    }
}