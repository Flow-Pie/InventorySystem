using Application.DTO.Request.Products;
using Application.DTO.Response.Products;
using Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllProductByIdHandler(Infrastructure.DataAccess.IDbContextFactory<AppDbContext> contextFactory) 
    : IRequestHandler<GetProductByIdQuery, GetProductResponseDTO>
{
    public async Task<GetProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var product = await dbContext.Products
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(l => l.Location)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }

        return new GetProductResponseDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Base64Image = product.Base64Image,
            CategoryId = product.CategoryId,
            LocationId = product.LocationId,
            Price = product.Price,
            DateAdded = product.DateAdded,
            Quantity = product.Quantity,
            SerialNumber = product.SerialNumber,
            Location = new GetLocationResponseDTO
            {
                Id = product.LocationId,
                Name = product.Location?.Name ?? string.Empty
            },
            Category = new GetCategoryResponseDTO
            {
                Id = product.CategoryId,
                Name = product.Category?.Name ?? string.Empty
            }
        };
    }
}