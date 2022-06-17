using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverflowEntities.Entities;
using StackOverflowEntities.Entities.Dtos;

namespace StackOverflowEntities.Endpoints
{
    public static class UsersEnpoints
    {
        public static WebApplication RegisterUsersEndpoints(this WebApplication app)
        {
            app.MapGet("users", async (StackOverflowContext db) =>
            {
                var users =  await db.Users
                    .AsNoTracking()
                    .Select(u => new UsersDto()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Questions = u.Questions.Count,
                        Replies = u.Replies.Count,
                        Comments = u.Comments.Count
                    })
                    .ToListAsync();

                return users;

            }).WithTags("Users");


            return app;
        }
    }
}
