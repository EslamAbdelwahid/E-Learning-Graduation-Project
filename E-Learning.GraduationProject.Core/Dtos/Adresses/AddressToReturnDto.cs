using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Adresses
{
    public class AddressToReturnDto
    {
        public int Id { get; set; }

        public string Street { get; set; } 
        public string City { get; set; } 
        public string Country { get; set; } 
    }
}
