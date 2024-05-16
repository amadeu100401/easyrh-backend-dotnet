using AutoMapper;
using EasyRh.Domain.Entities;
using EasyRh_Communication.Requests.User;

namespace EasyRh.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        //DomainToRequest();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>();
    }
}
