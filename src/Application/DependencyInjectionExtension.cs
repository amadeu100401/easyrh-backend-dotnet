#region USINGS
using EasyRh.Application.Services.AutoMapper;
using EasyRh.Application.UseCases.UserUseCases.Login;
using EasyRh.Application.UseCases.UserUseCases.Register;
using EasyRh.Domain.Security.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace EasyRh.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapping(services);
        AddUseCase(services);
    }

    private static void AddAutoMapping(IServiceCollection services)
    {
        var autoMapper = new AutoMapper.MapperConfiguration(options => 
            options.AddProfile(new AutoMapping())
        ).CreateMapper();

        services.AddScoped(options => autoMapper);
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
