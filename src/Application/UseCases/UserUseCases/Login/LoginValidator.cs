using EasyRh.Communication.Requests.Login;

using FluentValidation;
using EasyRh.Application.SharedValidators;

namespace EasyRh.Application.UseCases.UserUseCases.Login;

public class LoginValidator : AbstractValidator<RequestLoginJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email deve ser informado");
        When(user => !string.IsNullOrWhiteSpace(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("O email é inválido");
        });
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestLoginJson>());
    }
}
