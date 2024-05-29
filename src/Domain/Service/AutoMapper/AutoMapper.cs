using AutoMapper;
using EasyRh.Communication.Requests.User;
using EasyRh.Domain.Entities;

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
