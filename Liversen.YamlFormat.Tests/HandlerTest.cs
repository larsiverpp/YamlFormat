using System;
using System.IO;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Liversen.YamlFormat;

public static class HandlerTest
{
    static readonly string NewLine = Environment.NewLine;

    [Fact]
    public static async Task GivenFileAlreadyFormatted_WhenFormatting_ThenNoChanges()
    {
        var x = $"foo:{NewLine}- bar: zoo{NewLine}";
        var y = $"foo:{NewLine}- bar: zoo{NewLine}";

        (await Format(x)).ShouldBe(y);
    }

    [Fact]
    public static async Task GivenFileWithExtraSpaces_WhenFormatting_ThenFormattedWithoutExtraSpaces()
    {
        var x = $"foo:  bar{NewLine}";
        var y = $"foo: bar{NewLine}";

        (await Format(x)).ShouldBe(y);
    }

    static async Task<string> Format(string yamlInput)
    {
        using var dir = new TemporaryDirectory();
        var path = Path.Combine(dir.Location, "file.yaml");
        await File.WriteAllTextAsync(path, yamlInput);
        await new Handler().Handle(new(path, false, false));
        return await File.ReadAllTextAsync(path);
    }
}
