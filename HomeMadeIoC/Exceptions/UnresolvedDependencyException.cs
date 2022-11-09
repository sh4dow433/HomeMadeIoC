namespace HomeMadeIoC.Exceptions;

public class UnresolvedDependencyException : Exception
{
    public UnresolvedDependencyException(string type)
        : base($"Type {type} couldn't be resolved.")
    {
    }
}
