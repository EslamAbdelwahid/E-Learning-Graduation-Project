using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IOrderService
    {
        Task<OrderToReturnDto?> CreateOrderAsync(int studentId, string buyerEmail, string basketId);
        Task<IEnumerable<OrderToReturnDto>?> GetAllOrdersForSpecificUserAsync(string buyerEmail);
        Task<OrderToReturnDto?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId);


    }
}
