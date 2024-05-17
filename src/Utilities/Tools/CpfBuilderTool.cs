using Fare;

namespace Utilities.Tools;

public static class CpfBuilderTool
{
    private static Xeger _regexPattern = new Xeger(@"^\d{3}\.?(\d{3}\.?){2}\d{3}-?\d{2}$");

    public static string ValidCpfBuilder() => _regexPattern.Generate();
}
