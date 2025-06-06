using System.Threading.Tasks;

namespace Liversen.YamlFormat;

interface IHandler
{
    Task Handle(CommandArguments arguments);
}
