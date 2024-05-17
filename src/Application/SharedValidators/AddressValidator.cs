using EasyRh.Communication.Requests.Address;
using EasyRh.Exceptions;
using FluentValidation;

namespace EasyRh.Application.SharedValidators;

public class AddressValidator : AbstractValidator<RequestRegisterAddressJson>
{
    public AddressValidator()
    {
        RuleFor(address => address.Street).NotEmpty().WithMessage(ResourceErrorMessage.No_Street);
        RuleFor(address => address.City).NotEmpty().WithMessage(ResourceErrorMessage.No_City);
        RuleFor(address => address.State).NotEmpty().WithMessage(ResourceErrorMessage.No_State);
        RuleFor(address => address.Country).NotEmpty().WithMessage(ResourceErrorMessage.No_Country);
        RuleFor(address => address.ZipCode).SetValidator(new ZipCodeValidator<RequestRegisterAddressJson>());
    }
}
