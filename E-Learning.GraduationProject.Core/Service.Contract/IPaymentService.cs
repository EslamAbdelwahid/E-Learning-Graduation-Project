using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Baskets;
using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IPaymentService
    {
        Task<BasketToReturnDto> CreateOrUpdatePaymentIntent(string basketId);
        Task<OrderToReturnDto> HandlePaymentIntentSucceeded(string paymentIntentId, int studentId);
        Task<OrderToReturnDto> HandlePaymentIntentFailed(string paymentIntentId);
        Task<OrderToReturnDto> GetOrderByPaymentIntentId(string paymentIntentId);

    }
}
