using System.CommandLine;
using System.Threading.Tasks;

namespace Liversen.YamlFormat.Main;

static class Program
{
    static Task<int> Main(string[] args)
    {
        return RootCommandFactory.Create(new Handler()).InvokeAsync(args);
    }
}
