using Bogus;
using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Enum;
using Utilities.Tools;
using EasyRh.Communication.Requests.Address;
using Utilities.Builders.Address;

namespace Utilities.Builders.UserBuilders;

public class RequestRegisterUserBuilder
{
    public RequestRegisterUserJson Builder(int userRole = 0)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, f => f.Person.FullName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, BuilderPassword())
            .RuleFor(user => user.PhoneNumber, f => f.Person.Phone)
            .RuleFor(user => user.Cpf, BuilderCpf())
            .RuleFor(user => user.DateBirth, f => f.Person.DateOfBirth.ToString())
            .RuleFor(user => user.Gender, f => f.Person.Gender.ToString())
            .RuleFor(user => user.EnterpriseSerialIdentifier, Guid.NewGuid().ToString())
            .RuleFor(user => user.Role, GetUserRole(userRole))
            .RuleFor(user => user.Address, BuilderAddres());
    }

    private RequestRegisterAddressJson BuilderAddres()
    {
        var address = new RequestAddresRegisterBuilder().Builder();
        return address;
    }

    private string BuilderPassword()
    {
        var password = PasswordBuilderTool.ValidPasswordBuilder();

        return password;
    }

    private string BuilderCpf()
    {
        var cpf = CpfBuilderTool.ValidCpfBuilder();
        return cpf;
    }

    private RoleEnum GetUserRole(int role)
    {
        var roleUser = (RoleEnum)role;
        return roleUser;
    }
}
