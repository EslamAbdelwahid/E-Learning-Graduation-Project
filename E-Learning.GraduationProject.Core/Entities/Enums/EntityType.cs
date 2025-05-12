using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum EntityType
    {
        [EnumMember(Value = "Language Concept")]
        LanguageConcept,

        [EnumMember(Value = "Track Step")]
        TrackStep,

        [EnumMember(Value = "Problem Solving")]
        ProblemSolving
    }
}
