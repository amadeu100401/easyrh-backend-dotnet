using Bogus;
using EasyRh.Communication.Requests.Address;

namespace Utilities.Builders.Address;

public class RequestAddresRegisterBuilder
{
    public RequestRegisterAddressJson Builder()
    {
        return new Faker<RequestRegisterAddressJson>()
            .RuleFor(address => address.Street, f => f.Address.StreetName())
            .RuleFor(address => address.Number, f => f.Address.BuildingNumber())
            .RuleFor(address => address.Neighborhood, f => f.Address.StreetName())
            .RuleFor(address => address.Complement, f => f.Address.StreetAddress())
            .RuleFor(address => address.Complement, f => f.Address.City())
            .RuleFor(address => address.State, f => f.Address.StateAbbr())
            .RuleFor(address => address.ZipCode, f => f.Address.ZipCode())
            .RuleFor(address => address.Country, f => f.Address.Country());
    }
}
