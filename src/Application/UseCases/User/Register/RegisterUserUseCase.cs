using AutoMapper;
using EasyRh_Communication.Enum;
using EasyRh_Communication.Requests.User;
using EasyRh_Communication.Responses.User;

namespace EasyRh.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;

    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        Validations(request);

        var response = await SaveUser(request);

        return response;
    }

    private void Validations(RequestRegisterUserJson request)
    {
        var result = ValidateRequest(request);

        if(!result.IsValid)
        {
            throw new Exception("Ocorreu um erro na validação");
        }
    }

    private FluentValidation.Results.ValidationResult ValidateRequest(RequestRegisterUserJson request) 
    {
        var validade = new RegisterUserValidator();
        return validade.Validate(request);
    }

    private async Task<ResponseRegisterUserJson> SaveUser(RequestRegisterUserJson request)
    {
        var dataToSave = BuildUserEntity(request);

        return new ResponseRegisterUserJson
        {
            Name = request.Name,
            AccessToken = "teste",
            Role = RoleEnum.ADM
        };
    }
    private EasyRh.Domain.Entities.User BuildUserEntity(RequestRegisterUserJson request) => _mapper.Map<Domain.Entities.User>(request);

}
    