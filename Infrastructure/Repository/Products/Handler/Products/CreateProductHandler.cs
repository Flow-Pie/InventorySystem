using Application.DTO.Response;
using Application.Products.Commands.Product;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Products;

public class CreateProductHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<CreateProductCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var Product = await dbContext.Products.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.ProductModel.Name.ToLower()), cancellationToken : cancellationToken);
            
            if (Product == null)        
                return GeneralDbResponses.ItemAlreadyExists(request.ProductModel.Name);

            var data = request.ProductModel.Adapt(new Product());
            dbContext.Products.Add(data);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemCreated(request.ProductModel.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
