using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Notiifications.Queries.GetUserNotifications
{
    public class GetUserNotificationsQuery : IRequest<List<UserNotificationsViewModel>>
    {
        public string UserId { get; set; }
        public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, List<UserNotificationsViewModel>>
        {
            private readonly INotificationRepository notificationRepository;
            private readonly IMapper _mapper;
            public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
            {
                this.notificationRepository = notificationRepository;
                _mapper = mapper;
            }

            public async Task<List<UserNotificationsViewModel>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
            {
                var userNotifications = notificationRepository.GetUserNotifications(request.UserId);
                return await Task.FromResult(userNotifications.OrderByDescending(x => x.Id).ToList());
            }

        }
    }
}
