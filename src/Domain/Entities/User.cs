using EasyRh.Domain.Enum;

namespace EasyRh.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public string PhoneNumber { get; set; }
    public string Cpf { get; set; }
    public DateTime DateBirth { get; set; }
    public DateTime HireDate { get; set; }
    public string Gener { get; set; }
    public string EnterpriseSerialIdentifier { get; set; }
    public RoleDomainEnum Role { get; set; }
}
