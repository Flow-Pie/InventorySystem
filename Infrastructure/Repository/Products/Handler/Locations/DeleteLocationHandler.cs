using Application.DTO.Response;
using Application.Products.Commands.Location;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Locations;

public class DeleteLocationHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<DeleteLocationCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var data = await dbContext.Locations.FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken : cancellationToken);
            
            if (data ==null)        
                return GeneralDbResponses.ItemNotFound("Location");

            dbContext.Locations.Remove(data);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemDelete(data.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
