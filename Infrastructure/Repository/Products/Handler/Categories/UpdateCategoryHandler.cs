using Application.DTO.Response;
using Application.Products.Commands.Categories;
using Application.Service.Products.Commands;
using Domain.Entities;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Categories;

public class UpdateCategoryHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateCategoryCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //using keyword = the dbcontext will be disposed
            using var dbContext = contextFactory.CreateDbContext();
            var category = await dbContext.Categories.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.CategoryModel.Name.ToLower()), cancellationToken : cancellationToken);
            
            if (category == null)        
                return GeneralDbResponses.ItemAlreadyExists(request.CategoryModel.Name);

            dbContext.Entry(category).State = EntityState.Detached;
            var adaptData = request.CategoryModel.Adapt(new Category());
            dbContext.Categories.Update(adaptData);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponses.ItemUpdate(request.CategoryModel.Name); 
        } catch(Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }  

    }
}
