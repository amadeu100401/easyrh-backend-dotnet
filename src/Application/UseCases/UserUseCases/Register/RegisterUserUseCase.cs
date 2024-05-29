using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Service.UserService;
using EasyRh.Exceptions.ExceptionBase;

namespace EasyRh.Application.UseCases.UserUseCases.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IRegisterUserService _registerUserService;

    public RegisterUserUseCase(IRegisterUserService registerUserService)
    {
        _registerUserService = registerUserService;
    }

    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validations(request);

        var user = await _registerUserService.RegisterUser(request);

        return user;
    }

    private async Task Validations(RequestRegisterUserJson request)
    {
        var result = ValidateRequest(request);

        if(!result.IsValid)
        {
            var errorMessage = GetErrorMessage(result);
            throw new ErrorOnValidationException(errorMessage);
        }
    }

    private FluentValidation.Results.ValidationResult ValidateRequest(RequestRegisterUserJson request) 
    {
        var validade = new RegisterUserValidator();
        return validade.Validate(request);
    }

    private List<string> GetErrorMessage(FluentValidation.Results.ValidationResult result)
    {
        var errorMessage = new List<string>();

        foreach (var error in result.Errors)
        {
            errorMessage.Add(error.ErrorMessage);
        }

        return errorMessage;
    }
}
