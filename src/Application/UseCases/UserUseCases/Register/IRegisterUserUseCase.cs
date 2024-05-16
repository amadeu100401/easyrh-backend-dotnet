using EasyRh.Communication.Responses.User;
using EasyRh.Communication.Requests.User;

namespace EasyRh.Application.UseCases.UserUseCases.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseUserJson> Execute(RequestRegisterUserJson request);
}
