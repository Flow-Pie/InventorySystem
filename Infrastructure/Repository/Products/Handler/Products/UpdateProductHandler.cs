using Application.DTO.Response;
using Application.Products.Commands.Categories;
using Application.Products.Commands.Product;
using Application.Service.Products.Commands;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Categories;

public class UpdateProductHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateProductCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var product = await dbContext.Categories.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.ProductModel.Name.ToLower()), cancellationToken : cancellationToken);
            
            if (product == null)        
                return GeneralDbResponses.ItemAlreadyExists(request.ProductModel.Name);

            dbContext.Entry(product).State = EntityState.Detached;
            var adaptData = request.ProductModel.Adapt(new Product());
            dbContext.Products.Update(adaptData);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemUpdate(request.ProductModel.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
