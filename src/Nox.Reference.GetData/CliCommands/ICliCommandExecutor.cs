namespace Nox.Reference.GetData.CliCommands;

public interface ICliCommandExecutor
{
    void Run(string? commandName = null);
}