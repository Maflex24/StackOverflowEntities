using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverflowEntities.Entities;

namespace StackOverflowEntities.Endpoints
{
    public static class VoteEndpoints
    {
        public static WebApplication RegisterVoteEndpoints(this WebApplication app)
        {
            app.MapPost("vote", async (StackOverflowContext db, Guid elementId, Guid userId, int voteValue) =>
            {
                if (voteValue != -1 && voteValue != 1)
                    return null;

                var element = await db.QuestionReplyCommentModels.FirstOrDefaultAsync(e => e.Id == elementId);
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null || element == null)
                    return null;

                var vote = await db.Votes
                    .FirstOrDefaultAsync(v => v.UserId == userId && v.ElementId == elementId);

                if (vote == null)
                {
                    var newVote = new Vote()
                    {
                        ElementId = elementId,
                        UserId = userId,
                        Value = voteValue
                    };

                    await db.Votes.AddAsync(newVote);
                    element.Rating += voteValue;
                    await db.SaveChangesAsync();
                    return newVote;
                }

                var elementsSum = vote.Value + voteValue;
                if (elementsSum < -1 || elementsSum > 1) return null;

                vote.Value = elementsSum;
                element.Rating += voteValue;

                if (vote.Value == 0)
                {
                    db.Votes.Remove(vote);
                }
                await db.SaveChangesAsync();
                return vote;


            }).WithTags("Vote");

            return app;
        }
    }
}
