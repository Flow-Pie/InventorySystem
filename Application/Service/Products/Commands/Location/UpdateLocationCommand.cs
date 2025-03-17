using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Location;

public record UpdateLocationCommand (UpdateLocationRequestDTO LocationModel) : IRequest<ServiceResponse>;