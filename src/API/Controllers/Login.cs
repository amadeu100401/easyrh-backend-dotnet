using EasyRh.Application.UseCases.UserUseCases.Login;
using EasyRh.Communication.Requests.Login;
using EasyRh.Communication.Responses.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyRh.API.Controllers;

public class Login : EasyRhBaseController
{
    [HttpPost("/signup")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> DoLogin([FromBody] RequestLoginJson request,
        [FromServices] ILoginUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
