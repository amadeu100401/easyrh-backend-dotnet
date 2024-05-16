using EasyRh_Communication.Enum;
using EasyRh_Communication.Requests.Address;

namespace EasyRh_Communication.Requests.User;

public class RequestRegisterUserJson
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Cpf { get; set; }
    public string DateBirth { get; set; }
    public string Gener { get; set; }
    public Guid EnterpriseSerialIdentifier { get; set; }
    public RoleEnum Role { get; set; }
    public RequestRegisterAddressJson Address { get; set; }
}
