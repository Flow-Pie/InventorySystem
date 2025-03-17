using Application.DTO.Request.Products;
using MediatR;

namespace Application.Products.Queries.Categories;

public class  GetAllCategoriesQuery : IRequest<IEnumerable<GetCategoryResponseDTO>>
{
    
}