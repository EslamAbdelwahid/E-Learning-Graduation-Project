using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.PractiseProblems
{
    public class ParctiseProblemParames 
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Sort { get; set; }  // title ,  orderIndex
        public string? ProblemDifficultyLevel { get; set; } // difficulty [easy , medium , hard ]
        public string? PlatformName { get; set; } // platform [LeetCode , CodeForces , HackerRank ]

        public string? EntityType { get; set; }  // [LanguageConcept , TrackStep , ProblemSolving]

        private string? search; // title

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
