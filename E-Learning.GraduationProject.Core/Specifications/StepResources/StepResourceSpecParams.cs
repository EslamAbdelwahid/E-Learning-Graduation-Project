using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.StepResources
{
    public class StepResourceSpecParams
    {
        public int StepId { get; set; }
        public int PageSize { get; set; } = 4;
        public int PageIndex { get; set; } = 1;
        private string? searchByTitle;
        public string? SearchByTitle
        {
            get => searchByTitle;
            set => searchByTitle = value?.ToLower();
        }

        private string? searchByResourceType;
        public string? SearchByResourceType
        {
            get => searchByResourceType;
            set => searchByResourceType = value?.ToLower();
        }
    }
}
