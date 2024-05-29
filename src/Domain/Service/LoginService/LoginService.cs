using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Entities;
using EasyRh.Domain.Enum;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;
using EasyRh.Domain.Security.Token;
using EasyRh.Exceptions.ExceptionBase;

namespace EasyRh.Domain.Service.LoginService;

public class LoginService : ILoginService
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IAuthUser _auth;
    private readonly IAccessTokenGenerete _token;

    public LoginService(IUserReadOnlyRepository userReadOnlyRepository,
                        IAuthUser auth,
                        IAccessTokenGenerete token)
    {
        _repository = userReadOnlyRepository;
        _auth = auth;
        _token = token;
    }

    public async Task<ResponseUserJson> Login(RequestLoginJson request)
    {
        return await DoAuthentication(request);
    }


    private async Task<ResponseUserJson> DoAuthentication(RequestLoginJson request)
    {
        User user = await GetUserInDb(request.Email);

        var isValidPassword = DoComparePasswords(request.Password, user.Identifier.ToString(), user.Password, user.PasswordSalt);

        if (isValidPassword)
        {
            return BuildUserResponse(user);
        }
        else
        {
            throw new InvalidLoginException();
        }
    }

    private async Task<User> GetUserInDb(string email) => await _repository.GetUserByEmail(email);

    private bool DoComparePasswords(string requestPassword, string userGuid, string storedHash, string storedSalt)
        => _auth.ComparePasswords(requestPassword, userGuid, storedHash, storedSalt);

    private ResponseUserJson BuildUserResponse(User user)
    {
        return new ResponseUserJson
        {
            Name = user.Name,
            Role = (RoleEnum)user.Role,
            AccessToken = GenerateAccessToken(user)
        };
    }

    private string GenerateAccessToken(User user) => _token.Generate(user);
}
