using Application.DTO.Response;
using Application.Extensions;
using Application.Products.Commands.Categories;
using Application.Services.Products.Commands.Categories;
using Application.Services.Products.Orders;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Categories;

public class CancelOrderHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<CancelOrderCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var order = await dbContext.Orders.Where(_ => _.Id.Equals(request.OrderId)).FirstOrDefaultAsync(cancellationToken : cancellationToken);
            
            if (order ==null)        
                return new ServiceResponse(false, "Order not found");
            
            order.OrderState = OrderState.Canceled;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "Order canceled successfully");
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
