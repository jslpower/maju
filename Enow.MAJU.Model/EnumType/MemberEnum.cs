using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model.EnumType
{
    public enum 用户状态
    {
        正常 = 1,
        已停用
    }

    public enum 收藏类别
    {
        话题 = 0,
        资讯 = 1,
        保险 = 2
    }
    public enum 性别
    {
        男,
        女
    }
    public enum 家庭年收入
    {
        小于十万,
        十到二十万,
        二十到三十万,
        三十到四十万,
        四十到五十万,
        五十万以上
    }
    public enum 保险需求
    {
        重大疾病,
        教育理财,
        少儿保险,
        养老保险,
        海外理财

    }
}
