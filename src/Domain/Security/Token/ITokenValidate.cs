namespace EasyRh.Domain.Security.Token;

public interface ITokenValidate 
{
    public Guid Execute(string token);
}
