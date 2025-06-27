using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        public string BuyerMail { get; set; }
        public int BuyerId { get; set; } 
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }

        public string PaymentIntentId { get; set; }



        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<StudentProgress> Enrollments { get; set; }
    }
}
