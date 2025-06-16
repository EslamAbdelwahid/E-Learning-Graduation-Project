using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Orders
{
    public class OrderPaymentIntentSpecifications :BaseSpecification<Order,int>
    {
        public OrderPaymentIntentSpecifications(string paymentIntentId): base(
            O => O.PaymentIntentId == paymentIntentId
            )
        {
            
        }
    }
}
