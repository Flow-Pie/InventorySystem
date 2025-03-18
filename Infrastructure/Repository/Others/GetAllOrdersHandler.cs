using Application.DTO.Response.Order;
using Application.Extension.Identity;
using Domain.Entities;
using Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Products.Handlers.Orders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<GetOrderResponseDTO>>
    {
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetOrderResponseDTO>>
    {
        private readonly DataAccess.IDbContextFactory<AppDbContext> _contextFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllOrdersQueryHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory, UserManager<ApplicationUser> userManager)
        {
            _contextFactory = contextFactory;
            _userManager = userManager;
        }

        public async Task<IEnumerable<GetOrderResponseDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            var orders = await dbContext.Orders
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var productIds = orders.Select(o => o.ProductId).Distinct().ToList();
            var clientIds = orders.Select(o => o.ClientId).Distinct().ToList();

            var products = await dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);

            var users = await _userManager.Users
                .Where(u => clientIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, cancellationToken);

            return orders.Select(order => new GetOrderResponseDTO
            {
                Id = order.Id,
                ProductName = products.TryGetValue(order.ProductId, out var product) ? product.Name : null,
                Price = order.Price,
                DeliveringDate = order.DeliveringDate,
                OrderedDate = order.DateOrdered,
                ProductId = order.ProductId,
                ProductImage = products.TryGetValue(order.ProductId, out product) ? product.Base64Image : null,
                Quantity = order.Quantity,
                SerialNumber = products.TryGetValue(order.ProductId, out product) ? product.SerialNumber : null,
                ClientId = order.ClientId,
                ClientName = users.TryGetValue(order.ClientId, out var user) ? user.Name : null
            }).ToList();
        }
    }
}