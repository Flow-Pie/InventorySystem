using Application.DTO.Request.Orders;
using Application.DTO.Response;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

public class UpdateClientOrderedHandler
{
    private readonly Infrastructure.DataAccess.IDbContextFactory<AppDbContext> _contextFactory;

    public UpdateClientOrderedHandler(Infrastructure.DataAccess.IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ServiceResponse> Handle(UpdateClientOrderRequestDTO request, CancellationToken cancellationToken)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        try
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
            if (order == null)
            {
                return new ServiceResponse(false, "Order not found");
            }

            order.OrderState = request.OrderState ?? order.OrderState;
            if (request.DeliveringDate.HasValue)
            {
                order.DeliveringDate = request.DeliveringDate.Value;
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return new ServiceResponse(true, "Order updated successfully");
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, $"Error updating order: {ex.Message}");
        }
    }
}
