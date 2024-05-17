using EasyRh.Application.UseCases.UserUseCases.Register;
using EasyRh.Communication.Requests.User;
using Utilities.Builders.UserBuilders;

using FluentAssertions;

namespace ValidationTests.UserTests;

public class RegisterUserValidationTest
{
    [Fact]
    public void Success_Adm_Role()
    {
        var request = BuildRegisterUserRequest();

        var result = BuildRequestValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }

    private RequestRegisterUserJson BuildRegisterUserRequest() => new RequestRegisterUserBuilder().Builder();

    private RegisterUserValidator BuildRequestValidator() => new RegisterUserValidator();
}
