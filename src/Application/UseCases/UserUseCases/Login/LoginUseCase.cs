using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Service.LoginService;
using EasyRh.Exceptions.ExceptionBase;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly ILoginService _loginService;

    public LoginUseCase(ILoginService loginService)
    {
       _loginService = loginService;
    }

    public async Task<ResponseUserJson> Execute(RequestLoginJson request)
    {
        DoValidation(request);

        return await _loginService.Login(request);
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

    private List<String> GetErrorMessage(FluentValidation.Results.ValidationResult result)
    {
        var ErrorMessages = new List<String>();

        foreach (var error in result.Errors)
        {
            ErrorMessages.Add(error.ErrorMessage);
        }

        return ErrorMessages;
    }
}
