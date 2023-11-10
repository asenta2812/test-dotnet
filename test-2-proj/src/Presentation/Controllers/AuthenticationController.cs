using Application.Authentication.Commands.SignIn;
using Application.Authentication.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("auth")]
public class AuthenticationController : ApiBaseController
{
    [HttpPost("signin")]
    public async Task<ActionResult<string>> SignIn(SignInCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("signup")]
    public async Task<ActionResult<int>> SignUp(SignUpCommand command)
    {
        return await Mediator.Send(command);
    }
}