using Fare;

namespace Utilities.Tools;

public static class PasswordBuilderTool
{
    private static readonly Xeger _regexPattern = new Xeger("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,}$");

    public static string ValidPasswordBuilder() => _regexPattern.Generate();    
}
