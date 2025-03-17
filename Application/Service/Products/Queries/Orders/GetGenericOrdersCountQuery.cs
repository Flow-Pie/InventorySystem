using MediatR;

namespace Application.Service.Products.Queries;

public record GetGenericOrdersCountQuery (string UserId, bool IsAdmin = false) : IRequest<int>;