using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Entities;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IAuthUser _auth;

    public LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
        IAuthUser authUser)
    {
        _repository = userReadOnlyRepository;   
        _auth = authUser;
    }

    public async Task<ResponseUserJson> Execute(RequestLoginJson request)
    {
        DoValidation(request);

        var response = await DoAuthentication(request);

        return response;
    }

    private void DoValidation(RequestLoginJson request)
    {
        var result = DoRequestValidation(request);

        if(!result.IsValid)
        {
            throw new Exception("Não foi possível fazer o login: Credênciais inválidas");
        }
    }

    private FluentValidation.Results.ValidationResult DoRequestValidation(RequestLoginJson request) => new LoginValidator().Validate(request);

    private async Task<ResponseUserJson> DoAuthentication(RequestLoginJson request)
    {
        User user = await GetUserInDb(request.Email);

        var isValidPassword = DoComparePasswords(request.Password, user.Identifier.ToString(), user.Password, user.PasswordSalt);

        if(isValidPassword)
        {
            return BuildUserResponse(user);
        }
        else
        {
            throw new Exception("Não foi possível fazer o login: Credênciais inválidas");
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
            Role = (RoleEnum)Enum.Parse(typeof(RoleEnum), user.Role.ToString()),
            AccessToken = "TesteTokenTeste"
        };
    }

}
