using System.CommandLine;

namespace Liversen.YamlFormat;

static class RootCommandFactory
{
    public static RootCommand Create(IHandler handler)
    {
        var rootCommand = new RootCommand();
        var pathArgument = new Argument<string>("path")
        {
            Description = "File path"
        };
        rootCommand.Add(pathArgument);
        var indentSequencesOption = new Option<bool>("-i", "--indentSequences")
        {
            Description = "Indent sequences",
            DefaultValueFactory = _ => false
        };
        rootCommand.Add(indentSequencesOption);
        var preserveEmptyLinesAndCommentsArgument = new Option<bool>("-p", "--preserveEmptyLinesAndComments")
        {
            Description = "Preserve empty lines and comments",
            DefaultValueFactory = _ => false
        };
        rootCommand.Add(preserveEmptyLinesAndCommentsArgument);
        rootCommand.SetAction(async parseResult =>
        {
            await handler.Handle(
                new(
                    Path: parseResult.GetRequiredValue(pathArgument),
                    IndentSequences: parseResult.GetValue(indentSequencesOption),
                    PreserveEmptyLinesAndComments: parseResult.GetValue(preserveEmptyLinesAndCommentsArgument)));
        });

        return rootCommand;
    }
}
