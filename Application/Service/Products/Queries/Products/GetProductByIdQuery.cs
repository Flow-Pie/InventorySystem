using Application.DTO.Request.Products;
using MediatR;

namespace Application.DTO.Response.Products;

public record  GetProductByIdQuery(Guid Id) : IRequest<GetProductResponseDTO>;