using System.Drawing;
using ConsoleTables;

namespace CLI.UI.Utilities;

public class PrettyConsole
{
    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
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
        WriteLine(message,GetColor(Level.Info));
    }

    public static void WriteQuestion(string message)
    {
        WriteLine(message, GetColor(Level.Question));
    }

    public static void PrintTable<T>(IList<T> data, Level? level, Format format = Format.Default)
    {
        var color = GetColor(level);
        Console.ForegroundColor = color;

        var dataAttributes = typeof(T).GetProperties().Select(x => x.Name).ToList();

        var table = new ConsoleTable(dataAttributes.ToArray());

        foreach (var item in data)
        {
            var row = new List<string>();
            foreach (var attribute in dataAttributes)
            {
                row.Add(item.GetType().GetProperty(attribute).GetValue(item).ToString());
            }

            table.AddRow(row.ToArray());
        }

        table.Write(format);

        WriteLine("====================================", color);

        Console.ForegroundColor = ConsoleColor.White;
    }

    private static ConsoleColor GetColor(Level? level)
    {
        return level switch
        {
            Level.Info => ConsoleColor.DarkCyan,
            Level.Success => ConsoleColor.Green,
            Level.Error => ConsoleColor.Red,
            Level.Question => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };
    }
}

public enum Level
{
    Info,
    Success,
    Error,
    Question
}