using FluentValidation;
using EasyRh.Application.SharedValidators;
using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Enum;
using EasyRh.Exceptions;

namespace EasyRh.Application.UseCases.UserUseCases.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessage.No_Name);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceErrorMessage.No_Email);
        RuleFor(user => user.Cpf).SetValidator(new CpfValidatior<RequestRegisterUserJson>());
        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceErrorMessage.Invalid_Email);
        });
        RuleFor(user => user.DateBirth).NotEmpty().WithMessage(ResourceErrorMessage.No_BirthDate);
        When(user => !IsAdmUser(user.Role) && string.IsNullOrWhiteSpace(user.Role.ToString()), () =>
        {
            RuleFor(user => user.EnterpriseSerialIdentifier).NotEmpty()
                .WithMessage(ResourceErrorMessage.No_Enterprise_Identifier);
        });
        RuleFor(user => user.Address).SetValidator(new AddressValidator());
        When(user => !user.Role.Equals(RoleEnum.ADM), () =>
        {
            RuleFor(user => user.Role).NotEmpty().WithMessage(ResourceErrorMessage.No_Role);
        });
    }

    private bool IsAdmUser(RoleEnum role) => role.Equals(0);

}
