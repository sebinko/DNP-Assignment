namespace CLI.UI.Utilities;

public class PrettyConsole
{
    public static void WriteLine(string message, ConsoleColor? color)
    {
        Console.ForegroundColor = color ?? ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteError(string message)
    {
        WriteLine(message, ConsoleColor.Red);
    }

    public static void WriteSuccess(string message)
    {
        WriteLine(message, ConsoleColor.Green);
    }

    public static void WriteInfo(string message)
    {
        WriteLine(message, ConsoleColor.DarkCyan);
    }
    
    public static void WriteQuestion(string message)
    {
        WriteLine(message, ConsoleColor.DarkYellow);
    }
}