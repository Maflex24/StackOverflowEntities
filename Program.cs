using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflowEntities;
using StackOverflowEntities.Endpoints;
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
app.RegisterUsersEndpoints();
app.RegisterVoteEndpoints();

app.Run();
