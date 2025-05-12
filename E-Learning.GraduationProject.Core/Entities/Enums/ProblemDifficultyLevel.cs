using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum ProblemDifficultyLevel
    {
        [EnumMember(Value = "Easy")]
        Easy,

        [EnumMember(Value = "Medium")]
        Medium,

        [EnumMember(Value = "Hard")]
        Hard,

    }
}
