namespace Liversen.YamlFormat;

static class CastHelpers
{
    public static TBase UpCast<TBase, TDerived>(TDerived input)
        where TDerived : TBase =>
        input;
}
