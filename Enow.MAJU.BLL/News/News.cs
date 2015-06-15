using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    /// <summary>
    /// 资讯管理
    /// </summary>
    public class BNews
    {
        /// <summary>
        /// 获取资讯列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_News> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.NewsSearch SearchModel)
        {
            string FieldList = " NewsId,Title,PhotoPath,Context,Replys,Clicks,Collects,IsHot,IsTop,state,OperatorId,OperatorName,IssueTime,IsEssence";
            string TableName = " tbl_News ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.Title))
            {
                strWhere += " and Title like '%" + SearchModel.Title + "%'";
            }
            if (SearchModel.IsHot.HasValue)
            {
                strWhere += " and IsHot=" + (int)SearchModel.IsHot.Value + "";
            }
            if (SearchModel.IsTop.HasValue)
            {
                strWhere += " and IsTop=" + (int)SearchModel.IsTop.Value + "";
            }
            if (SearchModel.IsEssence.HasValue)
            {
                strWhere += " and IsEssence=" + (int)SearchModel.IsEssence.Value + "";
            }
            if (SearchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.StartDate.Value + "'";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.EndDate.Value.AddDays(1) + "'";
            }
            if (SearchModel.State.HasValue)
            {
                strWhere += " and state=" + (int)SearchModel.State.Value + "";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_News> q = rdc.ExecuteQuery<tbl_News>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }



        /// <summary>
        /// 根据ID返回资讯实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_News GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_News.FirstOrDefault(n => n.NewsId == Id);
                return model;
            }
        }

        /// <summary>
        /// 添加资讯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_News model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_News.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改资讯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_News model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_News.FirstOrDefault(n => n.NewsId == model.NewsId);
                if (m != null)
                {
                    m.Title = model.Title;
                    m.PhotoPath = model.PhotoPath;
                    m.Context = model.Context;
                    m.IsHot = model.IsHot;
                    m.IsTop = model.IsTop;
                    m.IsEssence = model.IsEssence;
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除资讯
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_News.FirstOrDefault(n => n.NewsId == Id);
                if (model != null)
                {
                    rdc.tbl_News.DeleteOnSubmit(model);
                    rdc.SubmitChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据枚举值更新资讯点击数、收藏数、回复数
        /// </summary>
        /// <param name="Id">NewsId</param>
        /// <param name="UpdateType">更新类别</param>
        /// <returns></returns>
        public static bool UpdateNumber(string Id, Model.EnumType.更新类别 UpdateType, Model.EnumType.操作符号 OperatorType)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_News.FirstOrDefault(n => n.NewsId == Id);
                if (m != null)
                {
                    switch (OperatorType)
                    {

                        case Enow.MAJU.Model.EnumType.操作符号.加:
                            #region 增加点击数、回复数、收藏数
                            switch (UpdateType)
                            {
                                case Model.EnumType.更新类别.回复:
                                    m.Replys = m.Replys + 1;
                                    break;
                                case Model.EnumType.更新类别.点击量:
                                    m.Clicks = m.Clicks + 1;
                                    break;
                                case Model.EnumType.更新类别.收藏:
                                    m.Collects = m.Collects + 1;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case Enow.MAJU.Model.EnumType.操作符号.减:
                            #region 删除资讯回复，减少资讯回复数
                            switch (UpdateType)
                            {
                                case Model.EnumType.更新类别.回复:
                                    if (m.Replys > 1)
                                    {
                                        m.Replys = m.Replys - 1;
                                    }
                                    else
                                    {
                                        m.Replys = 0;
                                    }
                                    break;
                                case Model.EnumType.更新类别.点击量:

                                    break;
                                case Model.EnumType.更新类别.收藏:

                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

    /// <summary>
    /// 资讯回复管理
    /// </summary>
    public class BNewsReply
    {
        /// <summary>
        /// 资讯回复分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_NewsReply> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.NewsReplySearch SearchModel)
        {
            string FieldList = " ReplyId,NewsId,MemberId,NickName,Context,IssueTime,Title";
            string TableName = " dt_NewsReply ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.NewsId))
            {
                strWhere += " and NewsId like '%" + SearchModel.NewsId + "%'";
            }

            if (SearchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.StartDate.Value + "'";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.EndDate.Value.AddDays(1) + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.KewWords))
            {
                strWhere += " and Context like '%" + SearchModel.KewWords + "%' ";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.NewsTitle))
            {
                strWhere += " and Title like '%" + SearchModel.NewsTitle + "%'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_NewsReply> q = rdc.ExecuteQuery<dt_NewsReply>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 新增回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_NewsReply model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_NewsReply.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <param name="Id">ReplyId</param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_NewsReply.FirstOrDefault(n => n.ReplyId == Id);
                if (m != null)
                {
                    #region 删除回复，资讯表回复数-1
                    BNews.UpdateNumber(m.NewsId, Model.EnumType.更新类别.回复, Model.EnumType.操作符号.减);
                    #endregion

                    rdc.tbl_NewsReply.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
