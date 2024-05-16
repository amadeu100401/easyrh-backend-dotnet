using EasyRh_Communication.Enum;

namespace EasyRh_Communication.Responses.User;

public class ResponseRegisterUserJson
{
    public string Name { get; set; }
    public RoleEnum? Role { get; set; }
    public string AccessToken { get; set; }
}
