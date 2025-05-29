using System.IO;
using YamlDotNet.Serialization;

namespace Liversen.YamlFormat;

static class Formatter
{
    public static string Format(
        string yamlInput,
        bool withIndentedSequences = false)
    {
        var deserializer = new DeserializerBuilder().Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlInput));

        var serializerBuilder = new SerializerBuilder();
        if (withIndentedSequences)
        {
            serializerBuilder = serializerBuilder.WithIndentedSequences();
        }

        var serializer = serializerBuilder.Build();
        var yamlOutput = serializer.Serialize(yamlObject);
        return yamlOutput;
    }
}
