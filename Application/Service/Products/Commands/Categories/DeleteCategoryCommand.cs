using Application.DTO.Response;
using MediatR;

namespace Application.Services.Products.Commands.Categories;

public record DeleteCategoryCommand (Guid Id) : IRequest<ServiceResponse>;
