using Application.DTO.Response;
using Application.Extensions;
using Application.Services.Products.Orders;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Orders;

public class CreateOrderHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) 
    : IRequestHandler<CreateOrderCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();

            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == request.OrderRequest.ProductId, cancellationToken);

            if (product == null)
                return new ServiceResponse(false, "Product not found");

            var order = request.OrderRequest.Adapt<Order>();

            order.TotalAmount = order.Quantity * product.Price;
            order.OrderState = OrderState.Processing;
            order.Price = product.Price;           

            dbContext.Orders.Add(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new ServiceResponse(true, "Order created successfully");
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, ex.Message);
        }
    }
}