using System.Reflection;
using System.Resources;

namespace opcs.App.Common;

public static class AppContext
{
    private static readonly ResourceManager ResourceManager;

    static AppContext()
    {
        ResourceManager = new ResourceManager("opcs.Resources.CodeMessages", Assembly.GetExecutingAssembly());
    }

    public static string GetCodeMessage(string code)
    {
        return ResourceManager.GetString(code) ?? code;
    }

    public static string GetFormattedCodeMessage(string code, params object[] args)
    {
        return string.Format(GetCodeMessage(code), args);
    }
}