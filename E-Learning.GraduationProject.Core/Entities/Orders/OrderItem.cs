using E_Learning.GraduationProject.Core.Entities.Instructors;
using StackExchange.Redis;

namespace E_Learning.GraduationProject.Core.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem(CourseOrder course, decimal price)
        {
            Course = course;
            Price = price;
        }
        public OrderItem()
        {
            
        }

        public CourseOrder Course { get; set; }
        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}