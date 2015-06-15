using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model.EnumType
{
    /// <summary>
    /// 面签状态枚举
    /// </summary>
    public enum 面签状态
    {
        已预约 = 0,
        待确定,
        已取消,
        已面签
    }

    public enum 方案状态
    {
        待发送 = 0,
        已发送 = 1,
        已预约

    }
}
