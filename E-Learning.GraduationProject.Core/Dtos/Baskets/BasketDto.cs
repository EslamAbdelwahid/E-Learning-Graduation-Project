using E_Learning.GraduationProject.Core.Dtos.BasketItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        // value
        public string UserId { get; set; } // User Id [Auth]
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
