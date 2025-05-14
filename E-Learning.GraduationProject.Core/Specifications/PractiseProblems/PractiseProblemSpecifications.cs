using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.PractiseProblems
{
    public class PractiseProblemSpecifications : BaseSpecification<PractiseProblem,int>
    {
        public PractiseProblemSpecifications(int id ) : base(
            PP => PP.Id == id
            )
        {
            
        }
        public PractiseProblemSpecifications()
        {
            
        }


    }
}
