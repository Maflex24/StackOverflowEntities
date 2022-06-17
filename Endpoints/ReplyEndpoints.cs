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
    public static class ReplyEndpoints
    {
        public static WebApplication RegisterReplyEndpoints(this WebApplication app)
        {
            app.MapPost("reply", async (StackOverflowContext db, ReplyPostModel postModel) =>
            {
                var newReply = new Reply()
                {
                    AuthorId = postModel.AuthorId,
                    QuestionId = postModel.QuestionId,
                    Content = postModel.Content,
                    Created = DateTime.Now
                };

                await db.Replies.AddAsync(newReply);
                await db.SaveChangesAsync();

                return newReply;

            }).WithTags("Replies");

            app.MapPut("reply", async (StackOverflowContext db, Guid replyId, string content) =>
            {
                var reply = db.Replies.FirstOrDefault(r => r.Id == replyId);
                reply.Content = content;

                await db.SaveChangesAsync();

            }).WithTags("Replies");

            app.MapDelete("reply", async (StackOverflowContext db, Guid replyId) =>
            {
                var newReply = new Reply()
                {
                    Id = replyId,
                    LastEdited = DateTime.Now
                };

                db.Replies.Remove(newReply);
                await db.SaveChangesAsync();

            }).WithTags("Replies");


            return app;
        }


    }
}