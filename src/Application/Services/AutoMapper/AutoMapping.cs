using AutoMapper;
using EasyRh.Communication.Requests.User;
using EasyRh.Domain.Entities;

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
