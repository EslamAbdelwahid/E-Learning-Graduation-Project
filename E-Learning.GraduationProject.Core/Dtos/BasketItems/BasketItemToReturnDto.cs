using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.BasketItems
{
    public class BasketItemToReturnDto
    {
      
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
