using EasyRh.Domain.Enum;

namespace EasyRh.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public String Cpf { get; set; }
    public string DateBirth { get; set; }
    public string Gener { get; set; }
    public Guid EnterpriseSerialIdentifier { get; set; } = Guid.NewGuid();
    public RoleDomainEnum Role { get; set; }
}
