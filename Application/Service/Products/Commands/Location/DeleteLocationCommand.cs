using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Location;
public record DeleteLocationCommand (Guid Id) : IRequest<ServiceResponse>;