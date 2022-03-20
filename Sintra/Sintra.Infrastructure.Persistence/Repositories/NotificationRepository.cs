using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Notiifications.Queries.GetUserNotifications;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : GenericRepositoryAsync<Notification>, INotificationRepository
    {
        private readonly DbSet<Notification> _products;
        private readonly IdentityContext context;
        private readonly IMapper mapper;
        private readonly IDapper dapper;

        public NotificationRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper) : base(dbContext, mapper)
        {
            _products = dbContext.Set<Notification>();
            context = dbContext;
            this.mapper = mapper;
            this.dapper = dapper;
        }

        public List<UserNotificationsViewModel> GetUserNotifications(string userId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("userId", userId);
            List<UserNotificationsViewModel> notifications = dapper.GetAll<UserNotificationsViewModel>
                (@$"select * from NotificationUsers nu
                    left join Notifications n on n.Id=nu.NotificationId
                    where n.StatusId=1 and nu.StatusId=1 and n.TypeId=1
                    and nu.UserId=@userId", p, CommandType.Text);
            return notifications;
        }
    }
}
