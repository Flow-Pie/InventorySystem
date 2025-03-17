using Application.DTO.Request.Products;
using Application.DTO.Response;
using Application.Products.Queries.Categories;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Categories;

public class GetAllCategoriesHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetCategoryResponseDTO>>
{   
    public async Task<IEnumerable<GetCategoryResponseDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var data = await dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        return data.Adapt<List<GetCategoryResponseDTO>>();
    }
}
