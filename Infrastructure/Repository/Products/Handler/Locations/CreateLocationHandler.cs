using Application.DTO.Response;
using Application.Products.Commands.Categories;
using Application.Service.Products.Commands;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Categories;

public class CreateLocationHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<CreateLocationCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var category = await dbContext.Locations.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.LocationModel.Name.ToLower()), cancellationToken : cancellationToken);
            
            if (category == null)        
                return GeneralDbResponses.ItemAlreadyExists(request.LocationModel.Name);

            var data = request.LocationModel.Adapt(new Location());
            dbContext.Locations.Add(data);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemCreated(request.LocationModel.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
