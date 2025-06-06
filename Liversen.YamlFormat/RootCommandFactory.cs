using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Liversen.YamlFormat;

static class RootCommandFactory
{
    public static RootCommand Create(IHandler handler)
    {
        var rootCommand = new RootCommand();
        rootCommand.AddArgument(new Argument<string>("path", "File path"));
        rootCommand.AddOption(new Option<bool>("-i", () => true, "Indent sequences"));
        rootCommand.AddOption(new Option<bool>("-p", () => true, "Preserve empty lines and comments"));
        rootCommand.Handler = CommandHandler.Create(async (string path, bool indentSequences, bool preserveEmptyLinesAndComments) =>
            await handler.Handle(
                new(
                    Path: path,
                    IndentSequences: indentSequences,
                    PreserveEmptyLinesAndComments: preserveEmptyLinesAndComments)));

        return rootCommand;
    }
}
