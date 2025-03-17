using Application.DTO.Response;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Products.Commands.Location;

namespace Infrastructure.Repository.Products.Handlers.Locations;

public class UpdateLocationHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateLocationCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var Location = await dbContext.Locations.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.LocationModel.Name.ToLower()), cancellationToken : cancellationToken);
            
            if (Location == null)        
                return GeneralDbResponses.ItemAlreadyExists(request.LocationModel.Name);

            dbContext.Entry(Location).State = EntityState.Detached;
            var adaptData = request.LocationModel.Adapt(new Location());
            dbContext.Locations.Update(adaptData);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemUpdate(request.LocationModel.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
