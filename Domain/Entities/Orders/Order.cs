using Domain.Entities; 

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}