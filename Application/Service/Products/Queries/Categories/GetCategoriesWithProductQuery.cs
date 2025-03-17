using Application.DTO.Request.Products;
using MediatR;

namespace Application.Products.Queries.Categories;

public class GetCategoriesWithProductQuery : IRequest<IEnumerable<GetCategoryResponseDTO>>
{
    
}