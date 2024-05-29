using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;

namespace EasyRh.Domain.Service.UserService;

public interface IRegisterUserService
{
    public Task<ResponseUserJson> RegisterUser(RequestRegisterUserJson request);
}
