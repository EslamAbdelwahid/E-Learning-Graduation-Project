using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Orders
{
    public class OrderToReturnDto
    {
        [JsonPropertyName("OrderId")]
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
  
        public string BuyerMail { get; set; }
        public int BuyerId { get; set; }
        public OrderStatus Status { get; set; } 
        public decimal TotalAmount { get; set; }

        public string PaymentIntentId { get; set; }



        // Navigation property
        public ICollection<OrderItemToReturnDto> OrderItems { get; set; }
    }
}
