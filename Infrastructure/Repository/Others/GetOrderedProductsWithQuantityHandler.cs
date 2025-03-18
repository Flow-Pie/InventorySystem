using Application.DTO.Response.Orders;
using Application.Service.Products.Queries.Orders;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

public class GetOrderedProductsWithQuantityHandler
{
    private readonly Infrastructure.DataAccess.IDbContextFactory<AppDbContext> _contextFactory;

    public GetOrderedProductsWithQuantityHandler(Infrastructure.DataAccess.IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<GetOrderedProductsWithQuantityResponseDTO>> Handle(GetOrderedProductsWithQuantityQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var ordersQuery = dbContext.Orders.AsNoTracking();
        if (!string.IsNullOrEmpty(request.UserId))
        {
            ordersQuery = ordersQuery.Where(order => order.ClientId.ToString() == request.UserId);
        }

        var orders = await ordersQuery
            .Where(order => order.DateOrdered >= DateTime.Today.AddMonths(-12))
            .ToListAsync(cancellationToken);

        var orderGroups = orders
            .GroupBy(order => order.ProductId)
            .Select(group => new
            {
                ProductId = group.Key,
                QuantityOrdered = group.Sum(order => order.Quantity)
            })
            .ToList();

        var productIds = orderGroups.Select(o => o.ProductId).ToList();
        var products = await dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken);

        var result = orderGroups.Select(group => new GetOrderedProductsWithQuantityResponseDTO
        {
            ProductName = products.TryGetValue(group.ProductId, out var name) ? name : "Unknown",
            QuantityOrdered = group.QuantityOrdered
        }).ToList();

        return result;
    }
}
