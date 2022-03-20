using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate
{
     public class UpdateOrderAccessoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class UpdateOrderAccessoryCommandHandler : IRequestHandler<UpdateOrderAccessoryCommand, Response<int>>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IDateTimeService dateTimeService;

            public UpdateOrderAccessoryCommandHandler(IOrderRepository orderRepository,
            IDateTimeService dateTimeService)
            {
                this.orderRepository= orderRepository;
                this.dateTimeService = dateTimeService;
            }
            public async Task<Response<int>> Handle(UpdateOrderAccessoryCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var orderAccessory = (await orderRepository.GetAsync<OrderAccessory>
                            (x => x.Id == command.Id, includeProperties: "Accessory")).FirstOrDefault();

                    if (orderAccessory == null)
                    {
                        throw new Exception($"Accessory Not Found.");
                    }
                    else
                    {
                        orderAccessory.LastRepeatDate = dateTimeService.NowUtc;
                        orderAccessory.RepeatDate = dateTimeService.NowUtc.AddDays(orderAccessory.Accessory.RepeatDay);
                        var res = orderRepository.UpdateOrderAccessory(orderAccessory);
                        return new Response<int>(orderAccessory.Id);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }
    }
}
