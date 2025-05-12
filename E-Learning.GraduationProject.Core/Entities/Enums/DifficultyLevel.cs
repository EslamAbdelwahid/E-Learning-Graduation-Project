using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum DifficultyLevel
    {
        [EnumMember(Value ="Basic")]
        Basic,

        [EnumMember(Value = "Intermediate")]
        Intermediate,

        [EnumMember(Value ="Advanced")]
        Advanced,
    }
}
