using Application.DTO.Request.Products;
using MediatR;

namespace Application.DTO.Response.Products;

public class GetProductQuery : IRequest<IEnumerable<GetProductResponseDTO>> { }