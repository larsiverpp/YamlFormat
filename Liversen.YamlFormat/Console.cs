using System.IO;

namespace Liversen.YamlFormat;

sealed class Console : IConsole
{
    public TextWriter Output => System.Console.Out;

    public TextWriter Error => System.Console.Error;

    public void Dispose()
    {
    }
}
