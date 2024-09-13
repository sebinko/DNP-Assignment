using CLI.UI;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using CLI.UI.Menu;
using InMemoryRepositories;
using Microsoft.Extensions.DependencyInjection;
using RepositoryContracts.Interfaces;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICommentRepository, CommentInMemoryRepository>()
    .AddSingleton<ILikeRepository, LikeInMemoryRepository>()
    .AddSingleton<IModeratorRepository, ModeratorInMemoryRepository>()
    .AddSingleton<IPostRepository, PostInMemoryRepository>()
    .AddSingleton<ISubforumRepository, SubforumInMemoryRepository>()
    .AddSingleton<IUserRepository, UserInMemoryRepository>()
    .AddSingleton<ListUserView>()
    .AddSingleton<CreateUserView>()
    .AddSingleton<UpdateUserView>()
    .AddSingleton<DeleteUserView>()
    .AddSingleton<ListPostView>()
    .AddSingleton<CreatePostView>()
    .AddSingleton<UpdatePostView>()
    .AddSingleton<DeletePostView>()
    .AddSingleton<ExitView>()
    .AddSingleton<CliApp>(provider => new CliApp(new Dictionary<string, IView>
    {
        { "1", provider.GetService<ListUserView>() },
        { "2", provider.GetService<CreateUserView>() },
        { "3", provider.GetService<UpdateUserView>() },
        { "4", provider.GetService<DeleteUserView>() },
        { "5", provider.GetService<ListPostView>() },
        { "6", provider.GetService<CreatePostView>() },
        { "7", provider.GetService<UpdatePostView>() },
        { "8", provider.GetService<DeletePostView>() },
        { "10", provider.GetService<ExitView>() }
    }))
    .BuildServiceProvider();

var app = serviceProvider.GetService<CliApp>();

if (app == null)
{
    Console.WriteLine("App is null");
    return;
}

await app.StartAsync();