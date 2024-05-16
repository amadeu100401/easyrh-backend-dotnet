using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;

namespace EasyRh.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
