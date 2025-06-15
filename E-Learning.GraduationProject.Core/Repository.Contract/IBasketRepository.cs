using E_Learning.GraduationProject.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string id);
        Task<Basket?> SetBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
