using Application.DTO.Request.Products;
using MediatR;

namespace Application.Products.Queries.Locations;

public class GetAllLocationsQuery : IRequest<IEnumerable<GetLocationResponseDTO>>
{
    
}
