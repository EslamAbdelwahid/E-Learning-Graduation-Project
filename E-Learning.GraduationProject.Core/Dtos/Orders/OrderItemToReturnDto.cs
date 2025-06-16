using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Orders
{
    public class OrderItemToReturnDto
    {
        [JsonPropertyName("OrderItemId")]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}
