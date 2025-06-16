using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecification<Order, int >
    {
        public OrderSpecifications(string buyerEmail ) : base (
            O => O.BuyerMail == buyerEmail
            )
        {
            ApplyIncludes();
        }
        public OrderSpecifications(string buyerEmail , int orderId) :base(
            O => O.Id == orderId && O.BuyerMail == buyerEmail
            )
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(O => O.OrderItems);
        }
    }
}
