using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
    /// <summary>
    /// 用户查询实体
    /// </summary>
    public class MemeberSearch
    {
        /// <summary>
        /// 省份ID
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// 区县ID
        /// </summary>
        public int? AreaId { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 会员昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public Model.EnumType.用户状态? Status { get; set; }
        /// <summary>
        /// 注册开始时间
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 注册结束时间
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
    
    }

    /// <summary>
    /// 会员收藏查询实体
    /// </summary>
    public class CollectSearch
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 收藏类别
        /// </summary>
        public Model.EnumType.收藏类别? RelatedType { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string RelatedId { get; set; }
    }
}
