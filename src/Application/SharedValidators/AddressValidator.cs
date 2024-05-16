using EasyRh.Communication.Requests.Address;
using FluentValidation;

namespace EasyRh.Application.SharedValidators;

public class AddressValidator : AbstractValidator<RequestRegisterAddressJson>
{
    public AddressValidator()
    {
        RuleFor(address => address.Street).NotEmpty().WithMessage("O nome da rua deve ser informado.");
        RuleFor(address => address.City).NotEmpty().WithMessage("O nome da cidade deve ser informado.");
        RuleFor(address => address.State).NotEmpty().WithMessage("O Estado deve ser informado");
        RuleFor(address => address.Country).NotEmpty().WithMessage("O país deve ser informado.");
        RuleFor(address => address.ZipCode).SetValidator(new ZipCodeValidator<RequestRegisterAddressJson>());
    }
}
