using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Orders
{
    public class CourseOrder
    {
        public CourseOrder(int courseId, string courseName, string pictureUrl)
        {
            CourseId = courseId;
            CourseName = courseName;
            PictureUrl = pictureUrl;
        }
        public CourseOrder()
        {
            
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }

    }
}
