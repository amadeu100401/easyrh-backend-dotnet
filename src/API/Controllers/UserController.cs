#region USING
using EasyRh.Application.UseCases.User.Register;
using EasyRh.Communication.Requests.User;
using EasyRh.Communication.Responses.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
#endregion

namespace EasyRh.API.Controllers;

public class UserController : EasyRhBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser([FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}
