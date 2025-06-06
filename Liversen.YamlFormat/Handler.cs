using System.IO;
using System.Threading.Tasks;

namespace Liversen.YamlFormat;

class Handler : IHandler
{
    public async Task Handle(CommandArguments arguments)
    {
        var yamlInput = await File.ReadAllTextAsync(arguments.Path);
        var yamlOutput = Formatter.Format(
            yamlInput,
            indentSequences: arguments.IndentSequences,
            preserveEmptyLinesAndComments: arguments.PreserveEmptyLinesAndComments);
        await File.WriteAllTextAsync(arguments.Path, yamlOutput);
    }
}
