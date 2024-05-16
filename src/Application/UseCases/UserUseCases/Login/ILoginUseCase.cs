using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public interface ILoginUseCase
{
    public Task<ResponseUserJson> Execute(RequestLoginJson request);
}
