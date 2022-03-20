using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.Features.Categories.Commands.CreateCategory;
using Sintra.Application.Features.Categories.Queries.GetAllCategories;
using Sintra.Application.Features.CategoryAccessories.Commands.AddCategoryAccessory;
using Sintra.Application.Features.CategoryAccessories.Queries.GetCategoryAccessories;
using Sintra.Application.Features.Clients.Queries.GetAllClients;
using Sintra.Application.Features.CreditCalls.Commands.CreateCreditCall;
using Sintra.Application.Features.CreditCalls.Queries.GetCreditCalls;
using Sintra.Application.Features.CreditCollectors.Queries.GetAllSellers;
using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Application.Features.ExpirationCalls.Commands.CreateExpirationCall;
using Sintra.Application.Features.ExpirationCalls.Queries.GetExpirationCalls;
using Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate;
using Sintra.Application.Features.OrderBonuses.Queries.GetAllOrderBonuses;
using Sintra.Application.Features.OrderBonuses.Queries.GetBonusTransactions;
using Sintra.Application.Features.Orders.Commands.CreateOrder;
using Sintra.Application.Features.Orders.Queries.GetAllOrders;
using Sintra.Application.FeaturesMobile.Orders.Queries.GetMobileOrders;
using Sintra.Application.Features.Orders.Queries.GetOrderAccessories;
using Sintra.Application.Features.ProductAccessories.Commands.AddProductAccessory;
using Sintra.Application.Features.ProductAccessories.Queries.GetAllProductAccessories;
using Sintra.Application.Features.Products.Commands.CreateProduct;
using Sintra.Application.Features.Products.Queries.GetAllProducts;
using Sintra.Application.Features.ProductTransfers.Queries.GetAllProductTransfers;
using Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts;
using Sintra.Application.Features.Regions.Commands.CreateRegion;
using Sintra.Application.Features.Regions.Queries.GetAllRegions;
using Sintra.Application.Features.Roles.Commands.CreateRole;
using Sintra.Application.Features.Roles.RoleModels;
using Sintra.Application.Features.Sellers.Queries.GetAllSellers;
using Sintra.Application.Features.Sellers.Queries.GetBalanceTransactions;
using Sintra.Application.Features.Sellers.Queries.GetCreditBalanceTransactions;
using Sintra.Application.Features.Transactions.Commands.CreateTransaction;
using Sintra.Application.Features.Transactions.Queries.GetAllTransactions;
using Sintra.Application.Features.Users.Queries;
using Sintra.Application.Features.Warehouses.Commands.AddWarehouseProduct;
using Sintra.Application.Features.Warehouses.Commands.CreateWarehouse;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouseProducts;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetPagedAllProductsQuery, GetAllProductsParameter>();

            CreateMap<ApplicationUser, GetAllUsersViewModel>().ReverseMap();

            CreateMap<IdentityRole<string>, GetAllRolesViewModel>().ReverseMap();
            CreateMap<CreateRoleCommand, IdentityRole<string>>().ConstructUsing(c => new IdentityRole<string>(c.Name)
            {
                Id = Guid.NewGuid().ToString()
            });

            CreateMap<Region, GetAllRegionsViewModel>().ReverseMap();
            CreateMap<CreateRegionCommand, Region>();

            CreateMap<Warehouse, GetAllWarehousesViewModel>().ReverseMap();
            CreateMap<CreateWarehouseCommand, Warehouse>();

            CreateMap<Transaction, GetAllTransactionsViewModel>().ReverseMap();
            CreateMap<CreateTransactionCommand, Transaction>();

            CreateMap<WarehouseProduct, GetAllWarehouseProductsViewModel>().ReverseMap();
            CreateMap<AddWarehouseProductCommand, WarehouseProduct>();

            CreateMap<Order, GetAllOrdersViewModel>().ReverseMap();
            CreateMap<CreateOrderCommand, Order>();

            CreateMap<ProductAccessory, GetAllProductAccessoriesViewModel>().ReverseMap();
            CreateMap<AddProductAccessoryCommand, ProductAccessory>();

            CreateMap<CategoryAccessory, GetCategoryAccessoriesViewModel>().ReverseMap();
            CreateMap<AddCategoryAccessoryCommand, CategoryAccessory>();

            CreateMap<OrderAccessory, GetOrderAccessoriesViewModel>().ReverseMap();

            CreateMap<ProductTransfer, GetAllProductTransfersViewModel>().ReverseMap();

            CreateMap<Credit, GetAllCreditsViewModel>().ReverseMap();

            CreateMap<CreditCall, GetCreditCallsViewModel>().ReverseMap();
            CreateMap<CreateCreditCallCommand, CreditCall>();

            CreateMap<Category, GetAllCategoriesViewModel>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<Client, GetAllClientsViewModel>().ReverseMap();

            CreateMap<ExpirationCall, GetExpirationCallsViewModel>().ReverseMap();
            CreateMap<CreateExpirationCallCommand, ExpirationCall>();

            CreateMap<OrderAccessory, GetAllOrderAccessoriesViewModel>().ReverseMap();

            CreateMap<ApplicationUser, GetAllSellersViewModel>().ReverseMap();
            CreateMap<ApplicationUser, GetAllCreditCollectorsViewModel>().ReverseMap();
            CreateMap<OrderBonus, GetAllOrderBonusesViewModel>().ReverseMap();

            CreateMap<EmployeeBonusTransaction, GetBonusTransactionsViewModel>().ReverseMap();
            CreateMap<EmployeeBalanceTransaction, GetBalanceTransactionsViewModel>().ReverseMap();
            CreateMap<EmployeeCreditBalanceTransaction, GetCreditBalanceTransactionsViewModel>().ReverseMap();

            #region MobileInjections
            CreateMap<Order, GetMobileOrdersViewModel>().ReverseMap();
            #endregion
        }
    }
}
