using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    #region 产品
    /// <summary>
    /// 产品
    /// </summary>
    public class BProduct
    {
        /// <summary>
        /// 产品分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_Product> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.ProductSearch SearchModel)
        {
            string FieldList = " ProductId,TypeId,ProductName,Company,Target,Advantage,OtherInfo,RelatedFile,InterviewNote,Collects,SortId,IssueTime";
            string TableName = " tbl_Product ";
            string OrderString = " order by SortId desc, IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.ProductName))
            {
                strWhere += " and ProductName like '%" + SearchModel.ProductName + "%'";
            }
            if (!string.IsNullOrEmpty(SearchModel.Company))
            {
                strWhere += " and Company like '%" + SearchModel.Company + "%'";
            }
            if (SearchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.StartDate.Value + "'";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.EndDate.Value.AddDays(1) + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_Product> q = rdc.ExecuteQuery<tbl_Product>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 根据产品ID返回产品实体
        /// </summary>
        /// <param name="Id">ProductId</param>
        /// <returns></returns>
        public static tbl_Product GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Product.FirstOrDefault(n => n.ProductId == Id);
                return model;
            }
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="model">产品实体</param>
        /// <returns></returns>
        public static bool Add(tbl_Product model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Product.InsertOnSubmit(model);
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
        /// 更新产品
        /// </summary>
        /// <param name="model">产品实体</param>
        /// <returns></returns>
        public static bool Update(tbl_Product model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Product.FirstOrDefault(n => n.ProductId == model.ProductId);
                if (m != null)
                {
                    m.TypeId = model.TypeId;
                    m.ProductName = model.ProductName;
                    m.Company = model.Company;
                    m.Target = model.Target;
                    m.Advantage = model.Advantage;
                    m.OtherInfo = model.OtherInfo;
                    m.RelatedFile = model.RelatedFile;
                    m.InterviewNote = model.InterviewNote;
                    m.SortId = model.SortId;
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
        /// 删除产品
        /// </summary>
        /// <param name="Id">ProductId</param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Product.FirstOrDefault(n => n.ProductId == Id);
                if (model != null)
                {
                    rdc.tbl_Product.DeleteOnSubmit(model);
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
        /// 更新产品收藏数
        /// </summary>
        /// <param name="Id">ProductId</param>
        /// <returns></returns>
        public static bool UpdateCollect(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Product.FirstOrDefault(n => n.ProductId == Id);
                if (model != null)
                {
                    model.Collects = model.Collects + 1;
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

    #endregion

    #region 产品类别
    /// <summary>
    /// 产品类别
    /// </summary>
    public class BProductType
    {
        /// <summary>
        /// 产品类别分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="KeyWord">类别名称关键字</param>
        /// <returns></returns>
        public static List<tbl_ProductType> GetList(ref int RowsCount, int PageSize, int PageIndex, string KeyWord)
        {
            string FieldList = " TypeId,TypeName,IssueTime,IsTop,SortId ";
            string TableName = " tbl_ProductType ";
            string OrderString = " order by IsTop desc,SortId desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                strWhere += " and TypeName like '%" + KeyWord + "%'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_ProductType> q = rdc.ExecuteQuery<tbl_ProductType>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 产品类别分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="KeyWord">类别名称关键字</param>
        /// <returns></returns>
        public static List<ProductType> GetViewList(ref int RowsCount, int PageSize, int PageIndex, string KeyWord)
        {
            string FieldList = " TypeId,TypeName,IssueTime,IsTop,SortId ";
            string TableName = " tbl_ProductType ";
            string OrderString = " order by IsTop desc,SortId desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                strWhere += " and TypeName like '%" + KeyWord + "%'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_ProductType> q = rdc.ExecuteQuery<tbl_ProductType>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                var l = new List<ProductType>();
                foreach (var m in q)
                {
                    l.Add(new ProductType()
                    {
                        TypeId = m.TypeId,
                        TypeName = m.TypeName,
                        SortId = m.SortId,
                        IsTop = m.IsTop,
                        IssueTime = m.IssueTime,
                        Products = rdc.ExecuteQuery<tbl_Product>(@"SELECT * FROM tbl_Product WHERE TypeId = '" + m.TypeId + "' ORDER BY SortId desc, IssueTime desc").ToList()
                    });
                }
                return l;
            }
        }

        /// <summary>
        /// 根据Id返回产品类别实体
        /// </summary>
        /// <param name="Id">TypeId</param>
        /// <returns></returns>
        public static tbl_ProductType GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ProductType.FirstOrDefault(n => n.TypeId == Id);
                return model;
            }
        }

        /// <summary>
        /// 新增产品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_ProductType model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_ProductType.InsertOnSubmit(model);
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
        /// 修改产品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_ProductType model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductType.FirstOrDefault(n => n.TypeId == model.TypeId);
                if (model != null)
                {
                    m.TypeName = model.TypeName;
                    m.SortId = model.SortId;
                    m.IsTop = model.IsTop;
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
        /// 删除分类
        /// </summary>
        /// <param name="Id">TypeId</param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ProductType.FirstOrDefault(n => n.TypeId == Id);
                if (model != null)
                {
                    rdc.tbl_ProductType.DeleteOnSubmit(model);
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

    #endregion


    #region 产品回复

    public class BProductReply
    {
        /// <summary>
        /// 产品回复分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_ProductReply> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.ProductReplySearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_ProductReply ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.ProductId))
            {
                strWhere += " and ProductId ='" + SearchModel.ProductId + "'";
            }
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_ProductReply> q = rdc.ExecuteQuery<dt_ProductReply>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 产品回复分页列表
        /// </summary>
        /// <param name="RecordCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_ProductReplyView> GetViewList(ref int RecordCount, int PageSize, int PageIndex, Model.ProductReplySearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_ProductReplyView ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.ProductId))
            {
                strWhere += " and ProductId ='" + SearchModel.ProductId + "'";
            }
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere += " and ProductName like '%" + SearchModel.KeyWords + "%'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Context))
            {
                strWhere += " and Context like '%" + SearchModel.Context + "%'";
            }
            if (SearchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.StartDate + "'";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.EndDate + "'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RecordCount = query.First<int>();
                List<dt_ProductReplyView> q = rdc.ExecuteQuery<dt_ProductReplyView>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 产品咨询列表
        /// </summary>
        /// <param name="MemberId">当前用户Id</param>
        /// <returns></returns>
        public static List<dt_ProductConsult> GetConsultList(string MemberId)
        {
            string FieldList = " distinct ProductId,ProductName,MemberId,max(issuetime) as issuetime  ";
            string TableName = " dt_ProductConsult ";
            string OrderString = " order by IssueTime desc";
            string GroupString = " group by ProductId,ProductName,MemberId";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(MemberId))
            {
                strWhere += " and MemberId='" + MemberId + "'";
            }
            using (FWDC rdc = new FWDC())
            {
                List<dt_ProductConsult> q = rdc.ExecuteQuery<dt_ProductConsult>(@" select " + FieldList + " from " + TableName + " where " + strWhere + " " + GroupString + " " + OrderString + "").ToList();
                return q;
            }
        }

        /// <summary>
        /// 新增回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_ProductReply model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_ProductReply.InsertOnSubmit(model);
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
        /// 根据产品回复ID获取实体
        /// </summary>
        /// <param name="ReplyId"></param>
        /// <returns></returns>
        public static tbl_ProductReply GetModel(string ReplyId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ProductReply.FirstOrDefault(n => n.ReplyId == ReplyId);
                return model;
            }
        }

        /// <summary>
        /// 判断我的产品咨询是否有未阅读的回复
        /// </summary>
        /// <param name="memberid">会员编号</param>
        /// <param name="productid">产品编号</param>
        /// <returns></returns>
        public static bool IsProductReplyAllRead(string memberid,string productid)
        {
            var RowsCount = 0;
            var s = "SELECT COUNT(1) FROM tbl_ProductReply WHERE IsRead='0' AND IsSysReply='1' AND MemberId='" + memberid + "'";
            if (!string.IsNullOrEmpty(productid)) s += (" AND ProductId='" + productid + "'");
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(s);
                RowsCount = query.First<int>();
            }
            return RowsCount > 0 ? false : true;
        }

        /// <summary>
        /// 根据产品咨询编号更新已读
        /// </summary>
        /// <param name="replyid"></param>
        /// <returns></returns>
        public static bool UpdateRead(string replyid)
        {
            using (FWDC rdc = new FWDC())
            {
                if (rdc.ExecuteCommand("UPDATE tbl_ProductReply SET IsRead='1' WHERE ReplyId='" + replyid + "'") > 0)
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


    #region 产品方案

    public class BProductSolution
    {
        /// <summary>
        /// 产品方案分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_ProductSolution> GetViewtList(ref int RowsCount, int PageSize, int PageIndex, Model.ProductSolutionSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_ProductSolution ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere += " and (ProductName like '%" + SearchModel.KeyWords + "%' or MemberName like '%" + SearchModel.KeyWords + "%')";
            }
            if (SearchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.IssueBeginTime.Value + "'";
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.IssueEndTime.Value.AddDays(1) + "'";
            }
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and MemberId='" + SearchModel.MemberId + "'";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_ProductSolution> q = rdc.ExecuteQuery<dt_ProductSolution>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 添加方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_ProductSolution model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_ProductSolution.InsertOnSubmit(model);
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
        /// 根据方案ID获取方案实体
        /// </summary>
        /// <param name="SolutionId"></param>
        /// <returns></returns>
        public static tbl_ProductSolution GetModel(string SolutionId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ProductSolution.FirstOrDefault(n => n.SolutionId == SolutionId);
                return model;
            }
        }

        /// <summary>
        /// 根据方案ID获取视图实体
        /// </summary>
        /// <param name="SolutionId"></param>
        /// <returns></returns>
        public static dt_ProductSolution GetViewModel(string SolutionId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_ProductSolution.FirstOrDefault(n => n.SolutionId == SolutionId);
                return model;
            }
        }

        /// <summary>
        /// 修改方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_ProductSolution model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductSolution.FirstOrDefault(n => n.SolutionId == model.SolutionId);
                if (m != null)
                {
                    m.SolutionId = model.SolutionId;
                    m.ProductId = model.ProductId;
                    m.MemberId = model.MemberId;
                    m.TargetName = model.TargetName;
                    m.Gender = model.Gender;
                    m.Age = model.Age;
                    m.Revenue = model.Revenue;
                    m.State = model.State;
                    m.Email = model.Email;
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
        /// 修改方案状态
        /// </summary>
        /// <param name="SolutionId">方案表ID</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static bool UpdateState(string SolutionId, byte State)
        {
            using (FWDC rdc =new FWDC())
            {
                var m = rdc.tbl_ProductSolution.FirstOrDefault(n => n.SolutionId == SolutionId);
                if (m != null)
                {
                    m.State = State;
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
        /// 删除方案
        /// </summary>
        /// <param name="SolutionId"></param>
        /// <returns></returns>
        public static bool Delete(string SolutionId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductSolution.FirstOrDefault(n => n.SolutionId == SolutionId);
                if (m != null)
                {
                    rdc.tbl_ProductSolution.DeleteOnSubmit(m);
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
    #endregion


    #region 产品预约、面签

    public class BProductInterview
    {
        /// <summary>
        /// 预约信息视图分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_ProductInterview> GetViewList(ref int RowsCount, int PageSize, int PageIndex, Model.ProductInterViewSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_ProductInterview ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.ProductId))
            {
                strWhere += " and ProductId ='" + SearchModel.ProductId + "'";
            }
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (SearchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)SearchModel.Status + "";
            }
            if (SearchModel.OrderTimeStart.HasValue)
            {
                strWhere += " and OrderTime>='" + SearchModel.OrderTimeStart.Value + "'";
            }
            if (SearchModel.OrderTimeEnd.HasValue)
            {
                strWhere += " and OrderTime<='" + SearchModel.OrderTimeEnd.Value.AddDays(1) + "'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere += " and (ProductName like '%" + SearchModel.KeyWords + "%' or MemberName like '%" + SearchModel.KeyWords + "%' ) ";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_ProductInterview> q = rdc.ExecuteQuery<dt_ProductInterview>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 面签分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_ProductInterview> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.ProductInterViewSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " tbl_ProductInterview ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.ProductId))
            {
                strWhere += " and ProductId ='" + SearchModel.ProductId + "'";
            }
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
            {
                strWhere += " and MemberId ='" + SearchModel.MemberId + "'";
            }
            if (SearchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)SearchModel.Status + "";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_ProductInterview> q = rdc.ExecuteQuery<tbl_ProductInterview>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 根据预约ID获取视图实体
        /// </summary>
        /// <param name="InterviewId"></param>
        /// <returns></returns>
        public static dt_ProductInterview GetViewModel(string InterviewId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_ProductInterview.FirstOrDefault(n => n.InterviewId == InterviewId);
                return model;
            }
        }

        /// <summary>
        /// 根据预约ID获取预约表实体
        /// </summary>
        /// <param name="InterviewId"></param>
        /// <returns></returns>
        public static tbl_ProductInterview GetModel(string InterviewId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ProductInterview.FirstOrDefault(n => n.InterviewId == InterviewId);
                return model;
            }
        }

        /// <summary>
        /// 新增面签预约
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_ProductInterview model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_ProductInterview.InsertOnSubmit(model);
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
        /// 修改面签预约
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_ProductInterview model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductInterview.FirstOrDefault(n => n.InterviewId == model.InterviewId);
                if (m != null)
                {
                    m.InterviewId = model.InterviewId;
                    m.OrderNo = model.OrderNo;
                    m.SolutionId = model.SolutionId;
                    m.InterviewTarget = model.InterviewTarget;
                    m.OrderTime = model.OrderTime;
                    m.OrderPlace = model.OrderPlace;
                    m.ContactTel = model.ContactTel;
                    m.Manager = model.Manager;
                    m.ManagerMob = model.ManagerMob;
                    m.Context = model.Context;
                    m.Status = model.Status;
                    m.InterviewNote = model.InterviewNote;
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
        /// 删除预约反馈
        /// </summary>
        /// <param name="SolutionId"></param>
        /// <returns></returns>
        public static bool Delete(string InterviewId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductInterview.FirstOrDefault(n => n.InterviewId == InterviewId);
                if (m != null)
                {
                    rdc.tbl_ProductInterview.DeleteOnSubmit(m);
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
        /// 判断是否存在面签
        /// </summary>
        /// <param name="solutionid"></param>
        /// <returns></returns>
        public static bool IsExists(string solutionid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ProductInterview.FirstOrDefault(n => n.SolutionId == solutionid);
                if (m != null)
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
/// <summary>
/// 产品类型
/// </summary>
public class ProductType : Enow.MAJU.BLL.tbl_ProductType
{
    public List<Enow.MAJU.BLL.tbl_Product> Products { get; set; }
}
