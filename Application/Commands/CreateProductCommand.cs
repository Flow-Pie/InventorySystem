using MediatR;

namespace Application.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}