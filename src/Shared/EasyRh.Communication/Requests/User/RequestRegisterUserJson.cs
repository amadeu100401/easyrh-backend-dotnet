﻿using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.Address;

namespace EasyRh.Communication.Requests.User;

public class RequestRegisterUserJson
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Cpf { get; set; }
    public string DateBirth { get; set; }
    public string Gender { get; set; }
    public string EnterpriseSerialIdentifier { get; set; }
    public RoleEnum Role { get; set; }
    public RequestRegisterAddressJson Address { get; set; }
}
