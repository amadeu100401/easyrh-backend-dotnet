using Microsoft.Extensions.Configuration;

namespace EasyRh.Infra.Extensions;

public static class ConfigurationExtension
{
    public static string ObtainConnectionString(this IConfiguration configuration)
    {
        return configuration?.GetSection("ConnectionStrings")!["ConnectionMySQLServer"];
    }
}
