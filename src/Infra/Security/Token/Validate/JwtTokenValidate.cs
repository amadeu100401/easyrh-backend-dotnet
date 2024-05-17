using EasyRh.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EasyRh.Infra.Security.Token.Validate;

public class JwtTokenValidate : JwtTokenHandler, ITokenValidate
{
    private readonly string _secret;

    public JwtTokenValidate(string secret) => _secret = secret;

    public Guid Execute(string token)
    {
        var principal = TokenValidate(token);

        return GetUserIdentifier(principal);
    }

    private ClaimsPrincipal TokenValidate(string token)
    {
        var tokenValidationParameters = BuildTokenParametersValidate();

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        return principal;
    }

    private TokenValidationParameters BuildTokenParametersValidate()
    {
        return new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateActor = true,
            ValidateIssuer = true,
            IssuerSigningKey = SecurityKey(_secret),
            ClockSkew = new TimeSpan(0)
        };
    }

    private Guid GetUserIdentifier(ClaimsPrincipal principal) => Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
}
