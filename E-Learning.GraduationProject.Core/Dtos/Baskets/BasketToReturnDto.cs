using E_Learning.GraduationProject.Core.Dtos.BasketItems;
using E_Learning.GraduationProject.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Baskets
{
    public class BasketToReturnDto
    {
        // key
        public string Id { get; set; }
        // value
        public string UserId { get; set; } // User Id [Auth]
        public List<BasketItemToReturnDto> Items { get; set; } 
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
