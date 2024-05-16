namespace EasyRh.Domain.Security.Cryptography;

public interface IAuthUser
{
    public bool ComparePasswords(string passwordToCompare, string userGuid, string storedHash, string storedSalt);
}
