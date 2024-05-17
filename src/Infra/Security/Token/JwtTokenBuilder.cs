using EasyRh.Domain.Entities;
using EasyRh.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EasyRh.Infra.Security.Token;

public class JwtTokenBuilder : JwtTokenHandler, IAccessTokenGenerete
{
    private readonly string _secret;
    private readonly uint _expirationTime;

    public JwtTokenBuilder(string secret, uint expirationTime)
    {
        _secret = secret;   
        _expirationTime = expirationTime;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, user.Identifier.ToString())
        };

        var tokenHandler = BuildTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTime),
            SigningCredentials = new SigningCredentials(SecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private JwtSecurityTokenHandler BuildTokenHandler()
    {
        return new JwtSecurityTokenHandler();
    }
}
