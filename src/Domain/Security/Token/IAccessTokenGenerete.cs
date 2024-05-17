using EasyRh.Domain.Entities;

namespace EasyRh.Domain.Security.Token;
public interface IAccessTokenGenerete
{
    public string Generate(User user);
}

