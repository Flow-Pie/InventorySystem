namespace Application.DTO
{
    public class ProductBaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SerialNumber { get; set; }
        public string Base64Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid LocationId { get; set; }
    }
}