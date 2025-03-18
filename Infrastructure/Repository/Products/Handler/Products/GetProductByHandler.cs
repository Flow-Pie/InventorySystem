using Application.DTO.Request.Products;
using Application.DTO.Response.Products;
using Infrastructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Products.Handlers.Products;

public class GetAllProductsHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<GetProductQuery, IEnumerable<GetProductResponseDTO>>
{   
    public async Task<IEnumerable<GetProductResponseDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var data = await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        return data.Adapt<List<GetProductResponseDTO>>();
    }
}
