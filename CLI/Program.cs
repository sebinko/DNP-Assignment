// See https://aka.ms/new-console-template for more information

using InMemoryRepositories;
using RepositoryContracts.Interfaces;

Console.WriteLine("Starting Cli App!");

ICommentRepository commentRepository = new CommentInMemoryRepository();
ILikeRepository likeRepository = new LikeInMemoryRepository();
IModeratorRepository moderatorRepository = new ModeratorInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
ISubforumRepository subforumRepository = new SubforumInMemoryRepository();
IUserRepository userRepository = new UserInMemoryRepository();

var cliApp = new CLI.UI.CliApp(
    commentRepository,
    likeRepository,
    moderatorRepository,
    postRepository,
    subforumRepository,
    userRepository
);

await cliApp.StartAsync();