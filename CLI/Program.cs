using CLI.UI;
using CLI.UI.Menu;
using CLI.UI.Views;
using CLI.UI.Views.ManageComments;
using CLI.UI.Views.ManageLikes;
using CLI.UI.Views.ManageModerators;
using CLI.UI.Views.ManagePosts;
using CLI.UI.Views.ManageSubforums;
using CLI.UI.Views.ManageUsers;
using CLI.UI.Views.SingleEntities;
using FileRepositories;
using InMemoryRepositories;
using Microsoft.Extensions.DependencyInjection;
using RepositoryContracts.Interfaces;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICommentRepository, CommentFileRepository>()
    .AddSingleton<ILikeRepository, LikeFileRepository>()
    .AddSingleton<IModeratorRepository, ModeratorFileRepository>()
    .AddSingleton<IPostRepository, PostFileRepository>()
    .AddSingleton<ISubforumRepository, SubforumFileRepository>()
    .AddSingleton<IUserRepository, UserFileRepository>()
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
    .AddSingleton<CreateCommentView>()
    .AddSingleton<ListCommentView>()
    .AddSingleton<UpdateCommentView>()
    .AddSingleton<DeleteCommentView>()
    .AddSingleton<SinglePostView>()
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
        { "21", provider.GetService<ListCommentView>() },
        { "22", provider.GetService<CreateCommentView>() },
        { "23", provider.GetService<UpdateCommentView>() },
        { "24", provider.GetService<DeleteCommentView>() },
        { "25", provider.GetService<SinglePostView>() },
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