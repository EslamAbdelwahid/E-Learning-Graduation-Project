using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum ProgressStatus
    {
        [EnumMember(Value = "Not Started")]
        NotStarted,

        [EnumMember(Value = "In Progress")]
        InProgress,

        [EnumMember(Value = "Completed")]
        Completed
    }
}
