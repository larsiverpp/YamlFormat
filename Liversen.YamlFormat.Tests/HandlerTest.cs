using System;
using System.IO;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Liversen.YamlFormat;

public class HandlerTest
{
    static readonly string NewLine = Environment.NewLine;
    readonly IHandler sut = CastHelpers.UpCast<IHandler, Handler>(new Handler());

    [Fact]
    public async Task GivenFileAlreadyFormatted_WhenFormatting_ThenNoChanges()
    {
        var x = $"foo:{NewLine}- bar: zoo{NewLine}";
        var y = $"foo:{NewLine}- bar: zoo{NewLine}";

        (await Format(x)).ShouldBe(y);
    }

    [Fact]
    public async Task GivenFileWithExtraSpaces_WhenFormatting_ThenFormattedWithoutExtraSpaces()
    {
        var x = $"foo:  bar{NewLine}";
        var y = $"foo: bar{NewLine}";

        (await Format(x)).ShouldBe(y);
    }

    async Task<string> Format(string yamlInput)
    {
        using var dir = new TemporaryDirectory();
        var path = Path.Combine(dir.Location, "file.yaml");
        await File.WriteAllTextAsync(path, yamlInput);
        await sut.Handle(new(path, false, false));
        return await File.ReadAllTextAsync(path);
    }
}
