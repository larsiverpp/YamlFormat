using Shouldly;
using Xunit;

namespace Liversen.YamlFormat;

public static class FormatterTest
{
    [Fact]
    public static void GivenInputAlreadyFormatted_WhenFormatting_ThenNoChanges()
    {
        const string input =
            """
            foo:
            - bar: zoo
            """;
        Formatter.Format(input).Trim().ShouldBe(input);
    }

    [Fact]
    public static void GivenInputWithExtraSpaces_WhenFormatting_ThenFormattedWithoutExtraSpaces()
    {
        const string content = "foo:  bar";
        const string expected = "foo: bar";
        Formatter.Format(content).Trim().ShouldBe(expected);
    }

    [Fact]
    public static void GivenInputWithNonIndentedSequences_WhenFormattingWithIndentedSequences_ThenFormattedWithIndentedSequence()
    {
        const string input =
            """
            foo:
            - bar: zoo
            """;
        const string output =
            """
            foo:
              - bar: zoo
            """;
        Formatter.Format(input, withIndentedSequences: true).Trim().ShouldBe(output);
    }
}
