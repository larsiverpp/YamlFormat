using System;
using Shouldly;
using Xunit;

namespace Liversen.YamlFormat;

public static class FormatterTest
{
    static readonly string NewLine = Environment.NewLine;

    [Fact]
    public static void GivenInputAlreadyFormatted_WhenFormatting_ThenNoChanges()
    {
        var x = $"foo:{NewLine}- bar: zoo{NewLine}";
        var y = $"foo:{NewLine}- bar: zoo{NewLine}";

        Formatter.Format(x).ShouldBe(y);
    }

    [Fact]
    public static void GivenInputWithExtraSpaces_WhenFormatting_ThenFormattedWithoutExtraSpaces()
    {
        var x = "foo:  bar";
        var y = $"foo: bar{NewLine}";

        Formatter.Format(x).ShouldBe(y);
    }

    [Fact]
    public static void GivenInputWithNonIndentedSequences_WhenFormattingWithIndentedSequences_ThenFormattedWithIndentedSequence()
    {
        var x = $"foo:{NewLine}- bar";
        var y = $"foo:{NewLine}  - bar{NewLine}";

        Formatter.Format(x, indentSequences: true).ShouldBe(y);
    }

    [Fact]
    public static void GivenInputWithEmptyLines_WhenFormattingWithPreserveEmptyLinesAndComments_ThenEmptyLinesPreserved()
    {
        var x = $"{NewLine}{NewLine}foo:{NewLine} {NewLine}- bar:  zoo{NewLine}{NewLine}";
        var y = $"foo:{NewLine}{NewLine}- bar: zoo{NewLine}";

        Formatter.Format(x, preserveEmptyLinesAndComments: true).ShouldBe(y);
    }

    [Fact]
    public static void GivenInputWithComments_WhenFormattingWithPreserveEmptyLinesAndComments_ThenCommentsPreserved()
    {
        var x = $"# ZOO{NewLine}foo:{NewLine}  #BAR{NewLine}- bar:  zoo";
        var y = $"# ZOO{NewLine}foo:{NewLine}#BAR{NewLine}- bar: zoo{NewLine}";

        Formatter.Format(x, preserveEmptyLinesAndComments: true).ShouldBe(y);
    }

    [Fact]
    public static void GivenInputWithEmptyLinesAndMultiLineString_WhenFormattingWithPreserveEmptyLinesAndComments_ThenArgumentException()
    {
        var x = $"foo: >{NewLine}  bar{NewLine}  zoo{NewLine}{NewLine}foo2: bar{NewLine}";

        var e = Should.Throw<ArgumentException>(() => Formatter.Format(x, preserveEmptyLinesAndComments: true));

        e.Message.ShouldBe(Formatter.UnexpectedNumberOfLinesErrorMessage);
    }
}
