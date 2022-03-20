using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.CreditCollectors.Commands.RecieveCreditBalance;
using Sintra.Application.Features.Sellers.Commands.RecieveBalance;
using Sintra.Application.Features.Users.Commands.DeleteUser;
using Sintra.Application.Features.Users.Commands.EditUser;
using Sintra.Application.Features.Users.Queries;
using Sintra.Application.Features.Users.Queries.GetEditUser;
using Sintra.Application.Features.Users.Queries.GetUserById;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class UserController : BaseController
    {

        [MyAuth]
        [HttpGet(Name = "İşçilər")]
        public IActionResult Employees()
        {
            return View();
        }

        [MyAuth]
        [HttpGet(Name = "İşçilər")]
        public async Task<JsonResult> GetEmployees()
        {
            return Json(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        [MyAuth]
        [HttpPost(Name = "Balansın qəbul edilməsi")]
        public async Task<JsonResult> ReceiveBalance(RecieveBalanceCommand command)
        {
            command.RecieverId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Balansın qəbul edilməsi")]
        public async Task<JsonResult> ReceiveCreditBalance(RecieveCreditBalanceCommand command)
        {
            command.RecieverId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpGet(Name = "İşçilərin redaktəsi")]
        public async Task<IActionResult> EditEmployee(string id)
        {
            return View(await Mediator.Send(new GetEditUserQuery { userId = id }));
        }
        [MyAuth]
        [HttpPost(Name = "İşçilərin redaktəsi")]
        public async Task<IActionResult> EditEmployee(EditUserCommand command, string[] selectedRoles)
        {
            command.SelectedRoles = selectedRoles;
            await Mediator.Send(command);
            return RedirectToAction("Employees","User");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeById(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserByIdCommand { Id = id }));
        }

    }
}
