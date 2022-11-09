namespace HomeMadeIoC.Exceptions;

public class InvalidLifeTimeException : Exception
{
    public InvalidLifeTimeException()
        : base($"A singleton can only depend on other singletons.")
    {

    }
}
