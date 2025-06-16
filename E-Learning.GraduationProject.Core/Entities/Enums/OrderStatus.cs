using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Enums
{
    public enum OrderStatus
    {
        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "processing")]
        Processing,

        [EnumMember(Value = "completed")]
        Completed,

        [EnumMember(Value = "failed")]
        Failed,

        [EnumMember(Value = "refunded")]
        Refunded,

        [EnumMember(Value = "cancelled")]
        Cancelled,

        [EnumMember(Value = "on_hold")]
        OnHold
    }
}
