using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.TrackSteps
{
    public class TrackStepSpecParams
    {
        public int PageSize { get; set; } = 4;
        public int PageIndex { get; set; } = 1;
        public int TrackId { get; set; }
        private string? searchByTitle;
        public string? SearchByTitle
        {
            get => searchByTitle;
            set => searchByTitle = value?.ToLower();
        }

    }
}
