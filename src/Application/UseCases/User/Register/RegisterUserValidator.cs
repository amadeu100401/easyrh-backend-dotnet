using FluentValidation;
using EasyRh.Application.SharedValidators;
using EasyRh_Communication.Requests.User;
using EasyRh_Communication.Enum;

namespace EasyRh.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Nome é obrigatório");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email é obrigatório");
        RuleFor(user => user.Cpf).SetValidator(new CpfValidatior<RequestRegisterUserJson>());
        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email inválido.");
        });
        RuleFor(user => user.DateBirth).NotEmpty().WithMessage("A data de nascimento é obrigatória.");
        When(user => !IsAdmUser(user.Role) && string.IsNullOrWhiteSpace(user.Role.ToString()), () =>
        {
            RuleFor(user => user.EnterpriseSerialIdentifier).NotEmpty()
                .WithMessage("O identificador da empresa deve ser informado quando o usuário não for um administrador.");
        });
        RuleFor(user => user.Gener).NotEmpty().WithMessage("O campo genero é obrigatório.");
        RuleFor(user => user.Address).SetValidator(new AddressValidator());
    }

    private bool IsAdmUser(RoleEnum role) => role.Equals(0);

}
