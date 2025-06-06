namespace Liversen.YamlFormat;

record CommandArguments(
    string Path,

    bool IndentSequences,

    bool PreserveEmptyLinesAndComments);
