using Application.DTO.Response.Order;
using Application.DTO.Response.Orders;
using Application.Service.Products.Queries.Orders;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

public class GetOrdersByIdHandler
{
    private readonly Infrastructure.DataAccess.IDbContextFactory<AppDbContext> _contextFactory;

    public GetOrdersByIdHandler(Infrastructure.DataAccess.IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<GetOrderResponseDTO>> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var products = await dbContext.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.Id.Equals(request.UserId)) 
            .Include(o => o.ProductId)
            .ToListAsync(cancellationToken);

        var result = orders.Select(order => new GetOrderResponseDTO
        {
            Id = order.Id,
            ProductName = products.FirstOrDefault(p => p.Id == order.ProductId)?.Name ?? "Unknown",
            Price = order.Price,
            DeliveringDate = order.DeliveringDate,
            OrderedDate = order.DateOrdered,
            ProductId = order.ProductId,
            ProductImage = products.FirstOrDefault(p => p.Id == order.ProductId)?.Base64Image ?? "N/A",
            Quantity = order.Quantity,
            SerialNumber = products.FirstOrDefault(p => p.Id == order.ProductId)?.SerialNumber ?? "N/A",
            State = order.OrderState
        }).ToList();

        return result;
    }
}
