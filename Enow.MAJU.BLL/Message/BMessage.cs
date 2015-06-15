using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    #region 系统消息
    /// <summary>
    /// 系统消息 
    /// </summary>
    public class BMessage
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_SysMessageSend> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.MessageSearch SearchModel)
        {

            string FieldList = " * ";
            string TableName = " dt_SysMessageSend ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Keywords))
            {
                strWhere += " and Context like '%" + SearchModel.Keywords + "%'";
            }
            if (SearchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.IssueBeginTime + "'";
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.IssueEndTime + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_SysMessageSend> q = rdc.ExecuteQuery<dt_SysMessageSend>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        public static tbl_SysMessage GetMmodel(int MessageId)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_SysMessage.FirstOrDefault(n => n.MessageId == MessageId);
                return m;
            }
        }

        #region 取得系统消息最大的ID
        public static int GetMaxId()
        {
            using (FWDC rdc=new FWDC())
            {
                var MaxId = (from s in rdc.tbl_SysMessage
                             select s.MessageId).Max();
                return MaxId;
            }
        }
        #endregion

        /// <summary>
        /// 消息列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageSize"></param>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public static List<tbl_SysMessage> GetMessageList(ref int RowsCount, int PageSize, int PageIndex, string KeyWords)
        {
            string FieldList = " * ";
            string TableName = " tbl_SysMessage ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(KeyWords))
            {
                strWhere += " and Context like '%" + KeyWords + "%'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_SysMessage> q = rdc.ExecuteQuery<tbl_SysMessage>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }

        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(tbl_SysMessage model)
        {
            int MessageId = 0;
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_SysMessage.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    MessageId = model.MessageId;
                }

                return MessageId;
            }
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int MessageSend(tbl_SysMessageSend model)
        {
            int SendId = 0;
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_SysMessageSend.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    SendId = model.SendId;
                }

                return SendId;
            }
        }

        /// <summary>
        /// 修改消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_SysMessage model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysMessage.FirstOrDefault(n => n.MessageId == model.MessageId);
                if (m != null)
                {
                    m.Context = model.Context;
                    m.OperatorId = model.OperatorId;
                    m.OperatorName = model.OperatorName;
                    m.IssueTime = model.IssueTime;
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
        /// 修改消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateRead(int sendid, int MessageId, string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysMessageSend.FirstOrDefault(n => n.MessageId == MessageId && n.SendId == sendid && n.MemberId == memberid);
                //var m = rdc.tbl_SysMessageSend.FirstOrDefault(n => n.MemberId == memberid);
                if (m != null)
                {
                    m.IsRead = char.Parse("1");
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region
        ///// <summary>
        ///// 获取消息实体
        ///// </summary>
        ///// <param name="MessageId"></param>
        ///// <returns></returns>
        //public static dt_SysMessageSend GetModel(int sendid, int MessageId, string memberid)
        //{
        //    using (FWDC rdc = new FWDC())
        //    {
        //        var model = rdc.dt_SysMessageSends.FirstOrDefault(n => n.MessageId == MessageId && n.SendId == sendid && n.MemberId == memberid);
        //        return model;
        //    }
        //}
        #endregion

        /// <summary>
        /// 获取消息实体
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public static dt_SysMessageSend GetModel( string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                
                var model = rdc.dt_SysMessageSends.FirstOrDefault(n => n.MemberId == memberid);
                return model;
            }
        }

        #region 根据MemberId和MessageId,获取sendId
        public static int GetSendId(int MessageId,string MemberId)
        {
            using (FWDC rdc=new FWDC())
            {
                int SendId = 0;
                var model = rdc.tbl_SysMessageSend.FirstOrDefault(n => n.MemberId == MemberId && n.MessageId == MessageId);
                if (model!=null)
                {
                    SendId = model.SendId;
                }
                return SendId;
            }
        }
        #endregion

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static bool Delete(int messageId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysMessage.FirstOrDefault(n => n.MessageId == messageId);
                if (model != null)
                {
                    rdc.tbl_SysMessage.DeleteOnSubmit(model);
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
        /// 判断系统消息是否全部已阅
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool IsSysMessageAllRead(string memberid)
        {
            var RowsCount = 0;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_SysMessage M WHERE M.MessageId IN (SELECT S.MessageId FROM tbl_SysMessageSend S WHERE S.MemberId='" + memberid + "' AND S.IsRead='0')");
                RowsCount = query.First<int>();
            }
            return RowsCount > 0 ? false : true;
        }
    }
    #endregion

    #region 消息回复

    public class BMessageReply
    {
        /// <summary>
        /// 回复列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_SysMessageReply> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.MessageSearch SearchModel)
        {

            string FieldList = " * ";
            string TableName = " tbl_SysMessageReply ";
            string OrderString = " order by IssueTime ";
            string strWhere = " 1=1";
            if (SearchModel.SendId > 0)
            {
                strWhere += " and SendId ='" + SearchModel.SendId + "'";
            }
          
            if (SearchModel.MessageId > 0)
            {
                strWhere += " and MessageId ='" + SearchModel.MessageId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Keywords))
            {
                strWhere += " and Context like '%" + SearchModel.Keywords + "%'";
            }
            if (SearchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.IssueBeginTime + "'";
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.IssueEndTime + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_SysMessageReply> q = rdc.ExecuteQuery<tbl_SysMessageReply>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取回复列表
        /// </summary>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_SysMessageReply> GetList(Model.MessageSearch SearchModel)
        {

            string FieldList = " * ";
            string TableName = " tbl_SysMessageReply ";
            string OrderString = " order by IssueTime ";
            string strWhere = " 1=1";
            if (SearchModel.SendId > 0)
            {
                strWhere += " and SendId ='" + SearchModel.SendId + "'";
            }

            if (SearchModel.MessageId > 0)
            {
                strWhere += " and MessageId ='" + SearchModel.MessageId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Keywords))
            {
                strWhere += " and Context like '%" + SearchModel.Keywords + "%'";
            }
            if (SearchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.IssueBeginTime + "'";
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.IssueEndTime + "'";
            }

            using (FWDC rdc = new FWDC())
            {
                List<tbl_SysMessageReply> q = rdc.ExecuteQuery<tbl_SysMessageReply>(@"SELECT " + FieldList + " FROM " + TableName + " WHERE " + strWhere + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 新增回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_SysMessageReply model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_SysMessageReply.InsertOnSubmit(model);
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
        /// 消息回复实体
        /// </summary>
        /// <param name="ReplyId"></param>
        /// <returns></returns>
        public static tbl_SysMessageReply GetModel(string ReplyId)
        {
            using (FWDC rdc=new FWDC())
            {
                var model = rdc.tbl_SysMessageReply.FirstOrDefault(n => n.ReplyId == ReplyId);
                return model;
            }
        }
    }
    #endregion
}
