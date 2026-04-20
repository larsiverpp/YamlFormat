using System;
using System.IO;

namespace Liversen.YamlFormat;

interface IConsole : IDisposable
{
    TextWriter Output { get; }

    TextWriter Error { get; }
}
