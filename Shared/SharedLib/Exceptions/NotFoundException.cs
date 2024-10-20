namespace SharedLib.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type classType)
        : base($"{classType.Name} not found")
    {
    }
}