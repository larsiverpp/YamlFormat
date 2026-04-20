using System.CommandLine;
using System.Threading.Tasks;

namespace Liversen.YamlFormat.Main;

internal static class Program
{
    internal static async Task<int> Main(string[] args)
    {
        using var console = new Console();
        return await MainInner(args, console);
    }

    internal static async Task<int> MainInner(string[] args, IConsole console)
    {
        var rootCommand = RootCommandFactory.Create(new Handler());
        var parseResult = rootCommand.Parse(args);
        var invocationConfiguration = new InvocationConfiguration
        {
            Output = console.Output,
            Error = console.Error
        };

        return await parseResult.InvokeAsync(invocationConfiguration);
    }
}
