using System.CommandLine;
using System.Threading;
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

        await sut.Parse(path).InvokeAsync(cancellationToken: CancellationToken.None);

        handler.Arguments.ShouldBe(new(
            Path: path,
            IndentSequences: false,
            PreserveEmptyLinesAndComments: false));
    }

    [Fact]
    public async Task GivenCommand_WhenInvokingWitShortOptions_ThenOptionsSet()
    {
        await sut.Parse("foobar -i -p").InvokeAsync(cancellationToken: CancellationToken.None);

        handler.Arguments.ShouldBe(new(
            Path: "foobar",
            IndentSequences: true,
            PreserveEmptyLinesAndComments: true));
    }

    [Fact]
    public async Task GivenCommand_WhenInvokingWitlongOptions_ThenOptionsSet()
    {
        await sut.Parse("foobar --indentSequences --preserveEmptyLinesAndComments").InvokeAsync(cancellationToken: CancellationToken.None);

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
