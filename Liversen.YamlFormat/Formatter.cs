using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace Liversen.YamlFormat;

static class Formatter
{
    public const string UnexpectedNumberOfLinesErrorMessage = "Unexpected number of lines after formatting, perhaps due to multi-line strings";

    public static string Format(
        string yamlInput,
        bool indentSequences = false,
        bool preserveEmptyLinesAndComments = false)
    {
        if (!preserveEmptyLinesAndComments)
        {
            return FormatInner(yamlInput, indentSequences: indentSequences);
        }

        var inputLines = GetLines(yamlInput.Trim()).ToImmutableArray();
        var inputLines2 = inputLines.Where(x => !IsEmptyLineOrComment(x)).ToImmutableArray();
        var yamlInput2 = string.Join(Environment.NewLine, inputLines2);

        var yamlOutput2 = FormatInner(yamlInput2, indentSequences: indentSequences);
        var outputLines2 = GetLines(yamlOutput2).ToImmutableArray();
        if (outputLines2.Length != inputLines2.Length)
        {
            throw new ArgumentException(UnexpectedNumberOfLinesErrorMessage);
        }

        var outputLines = new List<string>();

        var j = 0;
        foreach (var inputLine in inputLines)
        {
            if (IsEmptyLineOrComment(inputLine))
            {
                outputLines.Add(inputLine.Trim());
            }
            else
            {
                if (j < outputLines2.Length)
                {
                    outputLines.Add(outputLines2[j]);
                    ++j;
                }
            }
        }

        var yamlOutput = string.Join(Environment.NewLine, outputLines) + Environment.NewLine;

        return yamlOutput;
    }

    static string FormatInner(
        string yamlInput,
        bool indentSequences = false)
    {
        var deserializer = new DeserializerBuilder().Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlInput));

        var serializerBuilder = new SerializerBuilder();
        if (indentSequences)
        {
            serializerBuilder = serializerBuilder.WithIndentedSequences();
        }

        var serializer = serializerBuilder.Build();
        var yamlOutput = serializer.Serialize(yamlObject);

        return yamlOutput;
    }

    static bool IsEmptyLineOrComment(string line) =>
        IsEmptyLine(line) || IsCommentLine(line);

    static bool IsEmptyLine(string line) =>
        string.IsNullOrWhiteSpace(line);

    static bool IsCommentLine(string line) =>
        line.Trim().StartsWith('#');

    static IEnumerable<string> GetLines(string content)
    {
        using var sr = new StringReader(content);
        while (sr.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}
