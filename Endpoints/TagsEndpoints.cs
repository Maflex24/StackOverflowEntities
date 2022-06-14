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


            return app;
        }

    }
}