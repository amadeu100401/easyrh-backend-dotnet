using AutoMapper;
using EasyRh.Communication.Enum;
using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;
using EasyRh.Domain.Entities;
using EasyRh.Domain.Repositories;
using EasyRh.Domain.Repositories.UserRepository;
using EasyRh.Domain.Security.Cryptography;
using EasyRh.Domain.Security.Token;

namespace EasyRh.Application.UseCases.UserUseCases.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IAccessTokenGenerete _token;

    public RegisterUserUseCase(IMapper mapper, IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository, IPasswordEncrypter passwordEncrypter,
        IUnityOfWork unityOfWork, IAccessTokenGenerete token)
    {
        _mapper = mapper;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _passwordEncrypter = passwordEncrypter;
        _unityOfWork = unityOfWork;
        _token = token;
    }

    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validations(request);

        var user = await SaveUser(await BuildUserEntity(request));

        return BuildResponse(user);
    }

    private async Task Validations(RequestRegisterUserJson request)
    {
        var result = ValidateRequest(request);

        if(!result.IsValid)
        {
            throw new Exception("Ocorreu um erro na validação");
        }

        if(await GetUserByEamil(request.Email))
        {
            throw new Exception("Usuário já cadastrado");
        }
    }

    private FluentValidation.Results.ValidationResult ValidateRequest(RequestRegisterUserJson request) 
    {
        var validade = new RegisterUserValidator();
        return validade.Validate(request);
    }

    private async Task<bool> GetUserByEamil(string email)
    {
        return await _readRepository.ExistsActiveUserByEmail(email);
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

    private async Task<User> SaveUser(User data)
    {
        await _writeRepository.SaveUser(data);

        await _unityOfWork.commit();

        return data;
    }

    private ResponseUserJson BuildResponse(User user)
    {
        return new ResponseUserJson
        {
            Name = user.Name,
            Role = (RoleEnum)Enum.Parse(typeof(RoleEnum), user.Role.ToString()),
            AccessToken = _token.Generate(user)
        };
    }

}
