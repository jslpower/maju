using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
    public class TopicSearch
    {
        /// <summary>
        /// 话题标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 话题内容
        /// </summary>
        public string Context { get; set; }
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
        /// <summary>
        /// 是否前台显示
        /// </summary>
        public bool? isView { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }
    }

    public class ReplySearch
    {
        /// <summary>
        /// 话题ID
        /// </summary>
        public string TopicId { get; set; }
        /// <summary>
        /// 话题标题
        /// </summary>
        public string TopicTitle { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 回复时间-开始
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 回复时间-结束
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    public class TopSearch
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public EnumType.内容? T { get; set; }
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
        public bool? IsEssence { get; set; }
    }
}
