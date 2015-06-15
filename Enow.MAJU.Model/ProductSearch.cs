using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
    #region 产品查询实体
    /// <summary>
    /// 产品查询实体
    /// </summary>
    public class ProductSearch
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 发布开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 发布结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 产品类型编号
        /// </summary>
        public string TypeId { get; set; }
    }
    #endregion

    #region 产品回复查询实体
    /// <summary>
    /// 产品回复查询实体
    /// </summary>
    public class ProductReplySearch
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 发布开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 发布结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 产品名称关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 咨询内容关键字
        /// </summary>
        public string Context { get; set; }
    }
    #endregion

    #region 产品面签查询实体
    /// <summary>
    /// 产品面签查询实体
    /// </summary>
    public class ProductInterViewSearch
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 面签状态
        /// </summary>
        public Model.EnumType.面签状态? Status { get; set; }
        /// <summary>
        /// 面签查询关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 预约时间-开始
        /// </summary>
        public DateTime? OrderTimeStart { get; set; }
        /// <summary>
        /// 预约时间-结束
        /// </summary>
        public DateTime? OrderTimeEnd { get; set; }
    }
    #endregion

    #region 产品方案查询实体

    public class ProductSolutionSearch
    {
        /// <summary>
        /// 产品方案查询关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 方案添加日期-开始
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 方案添加日期-结束
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }
    }
    #endregion
}
