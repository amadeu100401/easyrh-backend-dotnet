using EasyRh.Communication.Requests.Login;

using FluentValidation;
using EasyRh.Application.SharedValidators;
using EasyRh.Exceptions;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public class LoginValidator : AbstractValidator<RequestLoginJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceErrorMessage.No_Email);
        When(user => !string.IsNullOrWhiteSpace(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceErrorMessage.Invalid_Email);
        });
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestLoginJson>());
    }
}
