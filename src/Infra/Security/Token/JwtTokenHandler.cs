using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace EasyRh.Infra.Security.Token;

public abstract class JwtTokenHandler
{
    protected SymmetricSecurityKey SecurityKey(string signinKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signinKey);  

        return new SymmetricSecurityKey(bytes);
    }
}
