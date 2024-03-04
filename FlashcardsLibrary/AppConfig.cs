using Microsoft.Extensions.Configuration;

namespace FlashcardsLibrary;
internal class AppConfig
{
    private static IConfiguration _iconfiguration;

    static AppConfig()
    {
        GetAppSettingsFile();
    }

    public static void GetAppSettingsFile()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            
        _iconfiguration = builder.Build();
    }

    public static string GetConnectionString()
    {
        return _iconfiguration.GetConnectionString("Default");
    }
}
