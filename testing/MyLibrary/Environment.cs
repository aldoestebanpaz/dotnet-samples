namespace MyLibrary;

// This class wraps the real calls that create/update the environment variables
// allowing it to be dependency injected into the code.
// This is an useful for unit testing purposes because the wrapper can be easily
// mocked in contrast to some built-in libraries.

public interface IEnvironmentWrapper
{
    string? GetVariable(string variableName);
    void SetVariable(string variableName, string value);
}

public class EnvironmentWrapper : IEnvironmentWrapper
{
    public string? GetVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName);
    }

    public void SetVariable(string variableName, string value)
    {
        Environment.SetEnvironmentVariable(variableName, value);
    }
}
