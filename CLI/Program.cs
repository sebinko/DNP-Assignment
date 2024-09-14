using CLI.UI;
using CLI.UI.ManageLikes;
using CLI.UI.ManageModerators;
using CLI.UI.ManagePosts;
using CLI.UI.ManageSubforums;
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
    .AddSingleton<CreateSubforumView>()
    .AddSingleton<ListSubforumView>()
    .AddSingleton<UpdateSubforumView>()
    .AddSingleton<DeleteSubforumView>()
    .AddSingleton<CreateModeratorView>()
    .AddSingleton<ListModeratorView>()
    .AddSingleton<UpdateModeratorView>()
    .AddSingleton<DeleteModeratorView>()
    .AddSingleton<ListLikeView>()
    .AddSingleton<CreateLikeView>()
    .AddSingleton<UpdateLikeView>()
    .AddSingleton<DeleteLikeView>()
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
        { "9", provider.GetService<ListSubforumView>() },
        { "10", provider.GetService<CreateSubforumView>() },
        { "11", provider.GetService<UpdateSubforumView>() },
        { "12", provider.GetService<DeleteSubforumView>() },
        { "13", provider.GetService<ListModeratorView>() },
        { "14", provider.GetService<CreateModeratorView>() },
        { "15", provider.GetService<UpdateModeratorView>() },
        { "16", provider.GetService<DeleteModeratorView>() },
        { "17", provider.GetService<ListLikeView>() },
        { "18", provider.GetService<CreateLikeView>() },
        { "19", provider.GetService<UpdateLikeView>() },
        { "20", provider.GetService<DeleteLikeView>() },
        { "Q", provider.GetService<ExitView>() }
    }))
    .BuildServiceProvider();

var app = serviceProvider.GetService<CliApp>();

if (app == null)
{
    Console.WriteLine("App is null");
    return;
}

await app.StartAsync();