using System.CommandLine;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Liversen.YamlFormat;

public class RootCommandFactoryTest
{
    readonly TestHandler handler;
    readonly RootCommand sut;

    public RootCommandFactoryTest()
    {
        handler = new();
        sut = RootCommandFactory.Create(handler);
    }

    [Fact]
    public async Task GivenCommand_WhenInvokingWithPath_ThenPathAndBooleansFalse()
    {
        var path = "foobar";

        await sut.InvokeAsync(path);

        handler.Arguments.ShouldBe(new(
            Path: path,
            IndentSequences: false,
            PreserveEmptyLinesAndComments: false));
    }

    [Fact(Skip = "Not working")]
    public async Task GivenCommand_WhenInvokingWithPathAndIndentSequences_ThenPathAndIndentSequencesTrue()
    {
        await sut.InvokeAsync("foobar -i");

        handler.Arguments.ShouldBe(new(
            Path: "foobar",
            IndentSequences: true,
            PreserveEmptyLinesAndComments: false));
    }

    sealed class TestHandler : IHandler
    {
        public CommandArguments? Arguments { get; private set; }

        public Task Handle(CommandArguments arguments)
        {
            Arguments = arguments;
            return Task.CompletedTask;
        }
    }
}
