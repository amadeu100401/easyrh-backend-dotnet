#region USING
using AutoMapper;
using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Entities;
using EasyRh.Domain.Repositories;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;
using EasyRh.Domain.Security.Token;
using EasyRh.Exceptions;
using EasyRh.Exceptions.ExceptionBase;
#endregion

namespace EasyRh.Domain.Service.UserService;

public class RegisterUserService : IRegisterUserService
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IAccessTokenGenerete _token;

    public RegisterUserService(IMapper mapper, 
                               IUserReadOnlyRepository userReadOnlyRepository,
                               IUserWriteOnlyRepository userWriteOnlyRepository,
                               IPasswordEncrypter passwordEncrypter,
                               IUnityOfWork unityOfWork,
                               IAccessTokenGenerete accessTokenGenerete)
    {
        _mapper = mapper;
        _readRepository = userReadOnlyRepository;
        _writeRepository = userWriteOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _unityOfWork = unityOfWork;
        _token = accessTokenGenerete;
    }

    public async Task<ResponseUserJson> RegisterUser(RequestRegisterUserJson request)
    {
        await Validate(request.Email);

        return await SaveUser(request);
    }

    private async Task Validate(string email)
    {
        if (await GetUserByEamil(email))
        {
            throw new ErrorOnValidationException(new List<string> { ResourceErrorMessage.User_Already_Registred });
        }
    }

    private async Task<bool> GetUserByEamil(string email)
    {
        return await _readRepository.ExistsActiveUserByEmail(email);
    }

    private async Task<ResponseUserJson> SaveUser(RequestRegisterUserJson request)
    {
        var user = await BuildUserEntity(request);

        await _writeRepository.SaveUser(user);

        await _unityOfWork.commit();

        return BuildResponse(user);
    }

    private async Task<User> BuildUserEntity(RequestRegisterUserJson request)
    {
        var entityUser = _mapper.Map<Domain.Entities.User>(request);

        entityUser.Identifier = Guid.NewGuid();

        // Obter a senha criptografada e o salt
        var encryptedPasswordInfo = await GetPasswordEncrypted(request.Password, entityUser.Identifier.ToString());

        // Definir a senha e o salt no objeto entityUser
        entityUser.Password = encryptedPasswordInfo.EncryptedPassword;
        entityUser.PasswordSalt = GetBase64ByByte(encryptedPasswordInfo.Salt);

        return entityUser;
    }

    private string GetBase64ByByte(byte[] bytes) => Convert.ToBase64String(bytes);

    private async Task<(string EncryptedPassword, byte[] Salt)> GetPasswordEncrypted(string password, string userGuid)
    {
        byte[] passwordSalt;
        var encryptedPassword = _passwordEncrypter.GetEcryptedPassword(password, userGuid, out passwordSalt);

        return (encryptedPassword, passwordSalt);
    }

    private ResponseUserJson BuildResponse(User user)
    {
        return new ResponseUserJson
        {
            Name = user.Name,
            Role = (RoleEnum)System.Enum.Parse(typeof(RoleEnum), user.Role.ToString()),
            AccessToken = _token.Generate(user)
        };
    }
}
