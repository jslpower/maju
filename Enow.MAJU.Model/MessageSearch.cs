using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
    /// <summary>
    /// 系统消息查询实体
    /// </summary>
    public class MessageSearch
    {
        /// <summary>
        /// 消息内容关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        ///回复时间
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
        /// <summary>
        /// 发送编号
        /// </summary>
        public int SendId { get; set; }
        /// <summary>
        /// 消息编号
        /// </summary>
        public int MessageId { get; set; }
    }
}
