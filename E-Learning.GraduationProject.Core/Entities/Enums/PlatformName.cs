using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum PlatformName
    {
        [EnumMember(Value = "Leet Code")]
        LeetCode,

        [EnumMember(Value = "Hacker Rank")]
        HackerRank,

        [EnumMember(Value = "CodeForces")]
        CodeForces,
    }
}
