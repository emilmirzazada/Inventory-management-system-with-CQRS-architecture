using Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate;
using Sintra.Application.Features.Orders.Commands.CreateOrder;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepositoryAsync<Order>
    {
        string UpdateOrderAccessory(OrderAccessory entity);
        string DeleteOrder(int id);
        Task<IEnumerable<OrderAccessory>> GetOrderAccessories(int orderId);
        string CreateOrder(CreateOrderCommand command);
        Task<IEnumerable<GetAllOrderAccessoriesViewModel>> GetAllOrderAccessories(string fromDate, string toDate);
        Task<IEnumerable<GetAllOrderAccessoriesViewModel>> GetDelayedOrderAccessories();
    }
}
