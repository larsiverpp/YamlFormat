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

    [Fact]
    public async Task GivenCommand_WhenInvokingWitShortOptions_ThenOptionsSet()
    {
        await sut.InvokeAsync("foobar -i -p");

        handler.Arguments.ShouldBe(new(
            Path: "foobar",
            IndentSequences: true,
            PreserveEmptyLinesAndComments: true));
    }

    [Fact]
    public async Task GivenCommand_WhenInvokingWitlongOptions_ThenOptionsSet()
    {
        await sut.InvokeAsync("foobar --indentSequences --preserveEmptyLinesAndComments");

        handler.Arguments.ShouldBe(new(
            Path: "foobar",
            IndentSequences: true,
            PreserveEmptyLinesAndComments: true));
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
