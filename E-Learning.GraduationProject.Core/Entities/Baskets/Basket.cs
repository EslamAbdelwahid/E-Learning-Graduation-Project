using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Baskets
{
    public class Basket
    {
        // redis 

        //key
        public string Id { get; set; } 
        //value
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string UserId { get; set; } // User Id [Auth]
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }

    }
}
