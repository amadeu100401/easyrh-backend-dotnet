using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Entities;
using EasyRh.Domain.Enum;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;
using EasyRh.Domain.Security.Token;
using EasyRh.Exceptions.ExceptionBase;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IAuthUser _auth;
    private readonly IAccessTokenGenerete _token;

    public LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
        IAuthUser authUser, IAccessTokenGenerete token)
    {
        _repository = userReadOnlyRepository;
        _auth = authUser;
        _token = token;
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

        if (!result.IsValid)
        {
            var errorMessage = GetErrorMessage(result);
            throw new ErrorOnValidationException(errorMessage);
        }
    }

    private FluentValidation.Results.ValidationResult DoRequestValidation(RequestLoginJson request) => new LoginValidator().Validate(request);

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

    private List<String> GetErrorMessage(FluentValidation.Results.ValidationResult result)
    {
        var ErrorMessages = new List<String>();

        foreach (var error in result.Errors)
        {
            ErrorMessages.Add(error.ErrorMessage);  
        }

        return ErrorMessages;
    }

    private async Task<User> GetUserInDb(string email) => await _repository.GetUserByEmail(email);

    private bool DoComparePasswords(string requestPassword, string userGuid, string storedHash, string storedSalt)
        => _auth.ComparePasswords(requestPassword, userGuid, storedHash, storedSalt);

    private ResponseUserJson BuildUserResponse(User user)
    {
        return new ResponseUserJson
        {
            Name = user.Name,
            Role = GetUserRole(user.Role),
            AccessToken = GenerateAccessToken(user)
        };
    }

    private RoleEnum GetUserRole(RoleDomainEnum role) => (RoleEnum)Enum.Parse(typeof(RoleEnum), role.ToString());

    private string GenerateAccessToken(User user) => _token.Generate(user);

}
