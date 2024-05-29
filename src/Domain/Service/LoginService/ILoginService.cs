using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;

namespace EasyRh.Domain.Service.LoginService;

public interface ILoginService
{
    public Task<ResponseUserJson> Login(RequestLoginJson request);
}
