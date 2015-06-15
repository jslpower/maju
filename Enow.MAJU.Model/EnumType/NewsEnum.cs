using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//资讯枚举
namespace Enow.MAJU.Model.EnumType
{
    public enum 是否热门
    {
        否 = 0,
        是
    }
    public enum 是否置顶
    {
        否 = 0,
        是
    }
    public enum 是否精华
    {
        否 = 0,
        是
    }


    public enum 更新类别
    {
        回复 = 0,
        点击量,
        收藏
    }

    public enum 状态
    {
        正常 = 0,
        已删除
    }

    public enum 操作符号
    {
        加,
        减
    }

    public enum 内容
    {
        话题,
        咨询
    }
}
