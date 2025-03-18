using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }=Guid.NewGuid();
       public Category Category { get; set; } = null;
       public Guid CategoryId { get; set; }
       public Location Location { get; set; } = null;
       public Guid LocationId { get; set; }
       [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Base64Image { get; set; }
        public string Name { get; set; }




    }
}