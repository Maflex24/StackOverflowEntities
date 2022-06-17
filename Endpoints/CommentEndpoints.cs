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
    public static class CommentsEndpoints
    {
        public static WebApplication RegisterCommentEndpoints(this WebApplication app)
        {
            app.MapPost("comment", async (StackOverflowContext db, CommentPostModel postModel) =>
            {
                var newComment = new Comment()
                {
                    AuthorId = postModel.AuthorId,
                    Content = postModel.Content,
                    Created = DateTime.Now
                };

                if (postModel.ReplyId != null)
                {
                    newComment.QuestionId = db.Replies.Where(r => r.Id == postModel.ReplyId).Select(r => r.QuestionId).FirstOrDefault();
                    newComment.ReplyId = postModel.ReplyId;
                }

                if (postModel.QuestionId != null)
                    newComment.QuestionId = postModel.QuestionId.Value;


                await db.Comments.AddAsync(newComment);
                await db.SaveChangesAsync();

                return newComment;

            }).WithTags("Comments");

            app.MapPut("comment", async (StackOverflowContext db, Guid commentId,  string content) =>
            {
                var comment = db.Comments.FirstOrDefault(r => r.Id == commentId);
                comment.Content = content;
                comment.LastEdited = DateTime.Now;

                await db.SaveChangesAsync();

            }).WithTags("Comments");

            app.MapDelete("comment", async (StackOverflowContext db, Guid commentId) =>
            {
                var newComment = new Reply()
                {
                    Id = commentId,
                };

                db.Replies.Remove(newComment);
                await db.SaveChangesAsync();

            }).WithTags("Comments");


            return app;
        }


    }
}