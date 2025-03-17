using Application.DTO.Request.Products;
using MediatR;

namespace Application.Products.Queries.Locations;
public class GetLocationWithProductQuery : IRequest<IEnumerable<GetLocationResponseDTO>>{}