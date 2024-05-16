using EasyRh_Communication.Requests.User;
using EasyRh_Communication.Responses.User;

namespace EasyRh.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
