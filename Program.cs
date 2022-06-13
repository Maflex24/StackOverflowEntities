using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflowEntities.Entities;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


builder.Services.AddDbContext<StackOverflowContext>(
    option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowEntitiesConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("question", async (StackOverflowContext db, [FromQueryAttribute] Guid questionId) =>
{
    var result = await db.Questions
        .AsNoTracking()
        .Include(q => q.Replies)
        .Include(q => q.Comments)
        .Include(q => q.Tags)
        .FirstAsync(q => q.Id == questionId);

    return result;
});

app.MapGet("questions", async (StackOverflowContext db) =>
{
    var results = await db.Questions
        .AsNoTracking()
        .Include(a => a.Author)
        .Select(q => new {Author = q.Author.Name, Question = q.Content, Date = q.Created.ToString("dd-MM-yyyy"), Rating = q.Rating, Replies = q.Replies})
        .ToListAsync();

    return results;
});

app.MapPost("questions", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid authorId) =>
{
    var question = new Question()
    {
        AuthorId = authorId,
        Content = content,
        Created = DateTime.Now
    };

    await db.Questions.AddAsync(question);
    await db.SaveChangesAsync();
});

app.MapPost("reply", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid questionId, [FromQueryAttribute] Guid authorId) =>
{
    var reply = new Reply()
    {
        AuthorId = authorId,
        Content = content,
        QuestionId = questionId,
        Created = DateTime.Now
    };

    await db.Replies.AddAsync(reply);
    await db.SaveChangesAsync();

    return reply;
});

app.MapPost("comment", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid elementId, [FromQueryAttribute] Guid authorId) =>
{
    var comment = new Comment()
    {
        AuthorId = authorId,
        Content = content,
        Created = DateTime.Now
    };

    var discriminator = await db.DiscriminatorViews
        .FirstAsync(e => e.Id == elementId);

    var value = discriminator.Discriminator == "Question"
        ? comment.QuestionId = elementId
        : comment.ReplyId = elementId;

    await db.AddAsync(comment);
    await db.SaveChangesAsync();

    return comment;
});

app.MapGet("users", async (StackOverflowContext db) =>
{
    var users = await db.Users
        .AsNoTracking()
        .Select(u => new {u.Id, u.Name})
        .ToListAsync();

    return users;
});

app.MapPost("users", async (StackOverflowContext db, string userName) =>
{
    var newUser = new User() {Name = userName};

    await db.Users.AddAsync(newUser);
    await db.SaveChangesAsync();

    return newUser;
});


using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<StackOverflowContext>();

if (!dbContext.Tags.Any())
{
    Tag.TagsSeed(dbContext);
    var users = await DataGenerator.UsersSeed(dbContext);
    var questions = await DataGenerator.QuestionsSeed(dbContext, users);
    var replies = await DataGenerator.ReplySeed(dbContext, users, questions);
    DataGenerator.CommentsSeed(dbContext);
}


app.Run();

