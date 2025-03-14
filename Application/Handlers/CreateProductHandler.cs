using Application.Commands;
using MediatR;

namespace Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Handle the command (e.g., create a product in the database)
            return Task.FromResult(1); // Return a product ID
        }
    }
}