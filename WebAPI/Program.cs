using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts.Interfaces;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GlobalExceptionMiddleware>();

// builder.Services.AddScoped<ILikeRepository, LikeFileRepository>();
// builder.Services.AddScoped<IUserRepository, UserFileRepository>();
// builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();
// builder.Services.AddScoped<IModeratorRepository, ModeratorFileRepository>();
// builder.Services.AddScoped<ISubforumRepository, SubforumFileRepository>();
// builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddSingleton<IUserRepository, UserInMemoryRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentInMemoryRepository>();
builder.Services.AddSingleton<IModeratorRepository, ModeratorInMemoryRepository>();
builder.Services.AddSingleton<ISubforumRepository, SubforumInMemoryRepository>();
builder.Services.AddSingleton<IPostRepository, PostInMemoryRepository>();
builder.Services.AddSingleton<ILikeRepository, LikeInMemoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();