namespace EasyRh.Domain.Security.Cryptography;

public interface IPasswordEncrypter
{
    public string GetEcryptedPassword(string password, string userGuid, out byte[] salt);
}
