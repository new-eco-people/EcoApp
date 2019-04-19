using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace Web.Ui.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        // [HttpPost("login")]
        // public async Task<IActionResult> LoginUser(LoginUserCommand command) {
        //     return Ok(await Mediator.Send(command));
        // }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command) {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }
    }
}