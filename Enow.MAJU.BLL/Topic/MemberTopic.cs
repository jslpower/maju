using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    /// <summary>
    /// 话题
    /// </summary>
    public class BMemberTopic
    {
        /// <summary>
        /// 话题分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_MemberTopic> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.TopicSearch SearchModel)
        {
            string FieldList = " TopicId,MemberId,Title,Context,Replys,Clicks,Collects,IsHot,IsTop,IsEssence,State,IssueTime,NickName,LatestMemberTopicReplys";
            string TableName = " dt_MemberTopic ";
            string OrderString = " order by IsTop desc, IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.Title))
            {
                strWhere += " and Title like '%" + SearchModel.Title + "%'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Context))
            {
                strWhere += " and Context like '%" + SearchModel.Context + "%'";
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
                strWhere += " and IsEssence =" + (int)SearchModel.IsEssence.Value + "";
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
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and memberid='" + SearchModel.MemberId + "'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                string querySql = @"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString;
                List<dt_MemberTopic> q = rdc.ExecuteQuery<dt_MemberTopic>(querySql).ToList();
                return q;
            }
        }

        /// <summary>
        /// 新增话题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MemberTopic model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MemberTopic.InsertOnSubmit(model);
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
        /// 根据TopicId获取视图实体
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        public static dt_MemberTopic GetViewModel(string TopicId)
        {
            using (FWDC rdc=new FWDC())
            {
                var model = rdc.dt_MemberTopic.FirstOrDefault(m => m.TopicId == TopicId);
                return model;
            }
        }
        /// <summary>
        /// 根据TopicId获取实体
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        public static tbl_MemberTopic GetModel(string TopicId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberTopic.FirstOrDefault(m => m.TopicId == TopicId);
                return model;
            }
        }

        /// <summary>
        /// 修改话题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_MemberTopic model)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_MemberTopic.FirstOrDefault(n => n.TopicId == model.TopicId);
                if (m != null)
                {
                    m.Title = model.Title;
                    m.IsTop = model.IsTop;
                    m.IsHot = model.IsHot;
                    m.IsEssence = model.IsEssence;
                    m.State = model.State;
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
        /// 删除话题
        /// </summary>
        /// <param name="Id">TopicId</param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberTopic.FirstOrDefault(n => n.TopicId == Id);
                if (m != null)
                {
                    rdc.tbl_MemberTopic.DeleteOnSubmit(m);
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
        /// 更新话题状态
        /// </summary>
        /// <param name="Id">话题ID</param>
        /// <param name="HotState">是否热门枚举</param>
        /// <param name="TopState">是否置顶枚举</param>
        /// <returns></returns>
        public static bool UpdateViews(string Id, Model.EnumType.是否热门 HotState, Model.EnumType.是否置顶 TopState)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberTopic.FirstOrDefault(n => n.TopicId == Id);
                if (m != null)
                {
                    m.IsHot = char.Parse(HotState.ToString());
                    m.IsTop = char.Parse(TopState.ToString());
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
        /// 根据枚举值更新话题点击数、收藏数、回复数
        /// </summary>
        /// <param name="Id">TopicId</param>
        /// <param name="UpdateType">更新类别</param>
        /// <returns></returns>
        public static bool UpdateNumber(string Id, Model.EnumType.更新类别 UpdateType, Model.EnumType.操作符号 OperatorType)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberTopic.FirstOrDefault(n => n.TopicId == Id);
                if (m != null)
                {
                    switch (OperatorType)
                    {
                        case Enow.MAJU.Model.EnumType.操作符号.加:
                            #region 增加
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
                            #region 删除话题回复，减少话题回复数量
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
        /// <summary>
        /// 首页分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_Top10> GetTops(ref int RowsCount, int PageSize, int PageIndex, Model.TopSearch SearchModel)
        {
            string FieldList = " T,TopicId,Title,IssueTime,HeadPhoto,IsHot,IsTop,IsEssence";
            string TableName = " dt_Top10 ";
            string OrderString = " order by IsTop,IssueTime desc";
            string strWhere = " 1=1";
            if (SearchModel.T.HasValue)
            {
                strWhere += " and T=" + (int)SearchModel.T.Value + "";
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
                strWhere += " and IsEssence='" + (SearchModel.IsEssence.Value ? 1 : 0) + "'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_Top10> q = rdc.ExecuteQuery<dt_Top10>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }

    #region 话题回复

    public class BMemberTopicReply
    {

        /// <summary>
        /// 话题评论分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_MemberTopicReply> GetViewList(ref int RowsCount, int PageSize, int PageIndex, Model.ReplySearch searchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_MemberTopicReply ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(searchModel.TopicId))
            {
                strWhere += " and topicId='" + searchModel.TopicId + "'";
            }
            if (searchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + searchModel.StartDate.Value + "'";
            }
            if (searchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + searchModel.EndDate.Value.AddDays(1) + "'";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.TopicTitle))
            {
                strWhere += " and Title like '%" + searchModel.TopicTitle + "%'";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Context))
            {
                strWhere += " and Context like '%" + searchModel.Context + "%'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_MemberTopicReply> q = rdc.ExecuteQuery<dt_MemberTopicReply>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        public static tbl_MemberTopicReply GetModel(string ReplyId)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_MemberTopicReply.FirstOrDefault(n => n.ReplyId == ReplyId);
                return m;
            }
        }

        /// <summary>
        /// 新增话题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MemberTopicReply model)
        {
            using (FWDC rdc=new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MemberTopicReply.InsertOnSubmit(model);
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
        /// 删除话题回复
        /// </summary>
        /// <param name="ReplyId"></param>
        /// <returns></returns>
        public static bool Delete(string ReplyId)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_MemberTopicReply.FirstOrDefault(n => n.ReplyId == ReplyId);
                if (m != null)
                {
                    rdc.tbl_MemberTopicReply.DeleteOnSubmit(m);
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
        /// 判断我的话题是否有未阅读的回复
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool IsMemberTopicReplyAllRead(string memberid)
        {
            if (string.IsNullOrEmpty(memberid)) return true;
            var RowsCount = 0;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_MemberTopicReply R WHERE R.IsRead='0' AND R.TopicId IN (SELECT T.TopicId FROM tbl_MemberTopic T WHERE T.MemberId='" + memberid + "' AND T.State='0')");
                RowsCount = query.First<int>();
            }
            return RowsCount > 0 ? false : true;
        }
        /// <summary>
        /// 根据话题编号获取最新回复数
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        public static int GetLatestMemberTopicReplys(string topicid)
        {
            var RowsCount = 0;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_MemberTopicReply R WHERE R.IsRead='0' AND R.TopicId ='" + topicid + "'");
                RowsCount = query.First<int>();
            }
            return RowsCount;
        }

        /// <summary>
        /// 根据话题编号更新已读
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        public static bool UpdateRead(string topicid,string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                if (rdc.ExecuteCommand("UPDATE tbl_MemberTopicReply SET IsRead='1' WHERE TopicId='" + topicid + "' AND TopicId IN (SELECT T.TopicId FROM tbl_MemberTopic T WHERE MemberId='"+memberid+"')") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    #endregion
}
