using EasyRh.Domain.Service.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRh.Domain;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection service, IConfiguration configuration)
    {
        AddAutoMapping(service);
        AddServices(service);
    }

    private static void AddAutoMapping(IServiceCollection services)
    {
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
            options.AddProfile(new AutoMapping())
        ).CreateMapper();

        services.AddScoped(options => autoMapper);
    }

    private static void AddServices(IServiceCollection service)
    {
        service.AddScoped<IRegisterUserService, RegisterUserService>();
    }
}
