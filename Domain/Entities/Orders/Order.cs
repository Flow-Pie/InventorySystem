using System.ComponentModel.DataAnnotations;
using Domain.Entities; 

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get;set; }=Guid.NewGuid();
        public DateTime DateOrdered { get; set; }=DateTime.Now;
        public DateTime DeliveringDate { get; set; }
        public Guid ProductId { get; set; }
        public string ClientId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderState { get; set; }

    }
}