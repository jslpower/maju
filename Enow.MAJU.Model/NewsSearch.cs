using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
    /// <summary>
    /// 资讯查询实体类
    /// </summary>
    public class NewsSearch
    {
        /// <summary>
        /// 资讯标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public Model.EnumType.是否置顶? IsTop { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public Model.EnumType.是否热门? IsHot { get; set; }
        /// <summary>
        /// 是否精华
        /// </summary>
        public Model.EnumType.是否精华? IsEssence { get; set; }
        /// <summary>
        /// 创建时间-开始
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 创建时间-结束
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Model.EnumType.状态? State { get; set; }
    }

    /// <summary>
    /// 资讯回复查询实体
    /// </summary>
    public class NewsReplySearch
    {
        /// <summary>
        /// 资讯ID
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 回复内容关键字
        /// </summary>
        public string KewWords { get; set; }
        /// <summary>
        /// 回复时间-开始
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 回复时间-结束
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 资讯标题
        /// </summary>
        public string NewsTitle { get; set; }
    }
}
