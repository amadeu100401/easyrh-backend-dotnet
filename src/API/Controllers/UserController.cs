#region USING
using EasyRh.Communication.Responses.User;
using EasyRh.Communication.Requests.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EasyRh.Application.UseCases.UserUseCases.Register;
#endregion

namespace EasyRh.API.Controllers;

public class UserController : EasyRhBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser([FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}
