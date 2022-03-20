using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sintra.Application.Features.Notiifications.Queries.GetUserNotifications;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IViewComponentResult Invoke()
        {
            return View(Mediator.Send(new GetUserNotificationsQuery {UserId= HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) }).Result);
        }
    }
}
