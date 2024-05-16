using EasyRh.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace EasyRh.Infra.Security.Cryptography;

public class PasswordEncrypter : IPasswordEncrypter, IAuthUser
{
    private readonly string _addtionalPasswordSign;
    private readonly int _keySizes = 64;
    private readonly int _interations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public PasswordEncrypter(string addtionalPasswordSign)
    {
        _addtionalPasswordSign = addtionalPasswordSign;
    }

    public string GetEcryptedPassword(string password, string userGuid, out byte[] salt)
    {
        var newPassword = GetAdditionalSecurity(password, userGuid);

        var hash = HashPassword(newPassword, out salt);

        return hash;
    }

    private string GetAdditionalSecurity(string password, string userGuid)
    {
        byte[] passwordBytes = GetPasswordByte(BuildPasswordToEncrypt(password, userGuid));   

        return GetBase64ByBytes(passwordBytes);
    }

    private string BuildPasswordToEncrypt(string password, string userGuid) => (GetComposedPassword(password, userGuid)) + GetGuidPasswordLength(password, userGuid);

    private byte[] GetPasswordByte(string password) => Encoding.UTF8.GetBytes(password);

    private string GetGuidPasswordLength(string password, string userGuid) => (password.Length + userGuid.Length).ToString();

    private string GetComposedPassword(string password, string userGuid) => password + _addtionalPasswordSign + userGuid;

    private string GetBase64ByBytes(byte[] bytes) => Convert.ToBase64String(bytes);

    private string HashPassword(string password, out byte[] salt)
    {
        salt = GetSalt();

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _interations,
            _hashAlgorithm,
            _keySizes);

        return Convert.ToHexString(hash);
    }

    private byte[] GetSalt() => RandomNumberGenerator.GetBytes(_keySizes);

    public bool ComparePasswords(string passwordToCompare, string userGuid , string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(GetAdditionalSecurity(passwordToCompare, userGuid), saltBytes, _interations, _hashAlgorithm, _keySizes);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(storedHash));
    }
}
