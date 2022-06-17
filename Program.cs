using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflowEntities;
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

app.RegisterQuestionsEndpoints();
app.RegisterTagsEndpoints();
app.RegisterReplyEndpoints();
app.RegisterCommentEndpoints();

app.Run();







//app.MapPost("questions", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid authorId) =>
//{
//    var question = new Question()
//    {
//        AuthorId = authorId,
//        Content = content,
//        Created = DateTime.Now
//    };

//    await db.Questions.AddAsync(question);
//    await db.SaveChangesAsync();
//});

//app.MapPost("reply", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid questionId, [FromQueryAttribute] Guid authorId) =>
//{
//    var reply = new Reply()
//    {
//        AuthorId = authorId,
//        Content = content,
//        QuestionId = questionId,
//        Created = DateTime.Now
//    };

//    await db.Replies.AddAsync(reply);
//    await db.SaveChangesAsync();

//    return reply;
//});

//app.MapPost("comment", async (StackOverflowContext db, [FromBodyAttribute] string content, [FromQueryAttribute] Guid elementId, [FromQueryAttribute] Guid authorId) =>
//{
//    var comment = new Comment()
//    {
//        AuthorId = authorId,
//        Content = content,
//        Created = DateTime.Now
//    };

//    var discriminator = await db.DiscriminatorViews
//        .FirstAsync(e => e.Id == elementId);

//    var value = discriminator.Discriminator == "Question"
//        ? comment.QuestionId = elementId
//        : comment.ReplyId = elementId;

//    await db.AddAsync(comment);
//    await db.SaveChangesAsync();

//    return comment;
//});

//app.MapGet("users", async (StackOverflowContext db) =>
//{
//    var users = await db.Users
//        .AsNoTracking()
//        .Select(u => new { u.Id, u.Name })
//        .ToListAsync();

//    return users;
//});

//app.MapPost("users", async (StackOverflowContext db, string userName) =>
//{
//    var newUser = new User() { Name = userName };

//    await db.Users.AddAsync(newUser);
//    await db.SaveChangesAsync();

//    return newUser;
//});