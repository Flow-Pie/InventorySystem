using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Service.Products.Commands;

public record CreateLocationCommand (AddLocationRequestDTO LocationModel) : IRequest<ServiceResponse>;