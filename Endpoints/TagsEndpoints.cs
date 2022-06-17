using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StackOverflowEntities.Entities;
using StackOverflowEntities.Entities.Dtos;
using StackOverflowEntities.Migrations;

namespace StackOverflowEntities
{
    public static class TagsEndpoints
    {
        public static WebApplication RegisterTagsEndpoints(this WebApplication app)
        {
            app.MapGet("tags", async (StackOverflowContext db) =>
            {
                var tags = await db.Tags
                    .Select(t => new {Id = t.Id, Name = t.Name, Questions = t.Questions.Count})
                    .ToListAsync();

                return tags;

            }).WithTags("Tags");


            app.MapGet("tag", async (StackOverflowContext db, int tagId, int page, int resultsPerPage) =>
            {
                var checkedTag = db.Tags
                    .FirstOrDefault(t => t.Id == tagId);

                var query = db.Questions
                    .AsNoTracking()
                    .Where(q => q.Tags.Contains(checkedTag));

                var skippedElements = resultsPerPage * (page - 1);
                var totalQuestionsQty = query.Count();

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

            }).WithTags("Tags");

            app.MapPost("tag", async (StackOverflowContext db, string tagName) =>
            {
                var existed = await db.Tags.Select(t => t.Name == tagName).FirstAsync();

                if (!existed)
                {
                    var newTag = new Tag() {Name = tagName};
                    await db.Tags.AddAsync(newTag);
                    await db.SaveChangesAsync();
                    return newTag;
                }

                return null;

            }).WithTags("Tags");

            app.MapPut("tag", async (StackOverflowContext db, int tagId, string newName) =>
            {
                var tag = db.Tags.FirstOrDefault(t => t.Id == tagId);
                tag.Name = newName;

                await db.SaveChangesAsync();

            }).WithTags("Tags");


            app.MapDelete("tag", async (StackOverflowContext db, int tagId) =>
            {
                var tag = new Tag() {Id = tagId};

                db.Tags.Remove(tag);
                await db.SaveChangesAsync();

            }).WithTags("Tags");

            return app;
        }


    }
}