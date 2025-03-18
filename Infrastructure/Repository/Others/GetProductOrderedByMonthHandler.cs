using System.Globalization;
using Application.DTO.Response.Orders;
using Application.Service.Products.Queries.Orders;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

public class GetProductsOrderedByMonthHandler
{
    private readonly Infrastructure.DataAccess.IDbContextFactory<AppDbContext> _contextFactory;

    public GetProductsOrderedByMonthHandler(Infrastructure.DataAccess.IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<GetProductOrderedByMonthsResponseDTO>> Handle(GetProductOrderedByMonthsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var query = dbContext.Orders.AsNoTracking().Where(o => o.DateOrdered >= DateTime.Today.AddMonths(-12));

        if (!string.IsNullOrEmpty(request.UserId))
        {
            query = query.Where(o => o.ClientId.ToString() == request.UserId);
        }

        var orders = await query.ToListAsync(cancellationToken);

        var groupedOrders = orders
            .GroupBy(o => new { o.DateOrdered.Year, o.DateOrdered.Month })
            .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
            .Select(g => new GetProductOrderedByMonthsResponseDTO
            {
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),              
                TotalAmount = g.Sum(o => o.TotalAmount)
            })
            .ToList();

        return groupedOrders;
    }
}
