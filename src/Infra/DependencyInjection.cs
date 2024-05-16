#region USING
using EasyRh.Domain.Repositories;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;
using EasyRh.Infra.DataAccess;
using EasyRh.Infra.DataAccess.Repositories;
using EasyRh.Infra.Extensions;
using EasyRh.Infra.Security.Cryptography;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
#endregion

namespace EasyRh.Infra;

public static class DependencyInjection
{
    public static void AddInfraestruture(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext_MySql(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
        AddPasswordSecurity(services, configuration);
    }

    private static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ObtainConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

        services.AddDbContext<EasyRhDbContext>(dbContextOptions =>              
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            var connectionString = configuration.ObtainConnectionString();

            options
            .AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("EasyRh.Infra")) //Informando onde estão os arquivos das Migrations
            .For.All();
        });
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();

        //UnityOfWork
        service.AddScoped<IUnityOfWork, UnityOfWork>();
    }

    private static void AddPasswordSecurity(IServiceCollection services, IConfiguration configuration)
    {
        var additionalPasswordSign = configuration.GetSection("Setting:Password:AdditionalKey").Value;

        services.AddScoped<IPasswordEncrypter>(options => new PasswordEncrypter(additionalPasswordSign));
        services.AddScoped<IAuthUser>(options => new PasswordEncrypter(additionalPasswordSign));
    }
}
