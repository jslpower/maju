using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    /// <summary>
    /// 广告管理
    /// </summary>
    public class BAdv
    {
        /// <summary>
        /// 广告分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_Adv> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.AdvSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " tbl_Adv ";
            string OrderString = " order by TypeId desc,SortId asc";
            string strWhere = " 1=1";
            if (!String.IsNullOrWhiteSpace(SearchModel.Title))
            {
                strWhere += " and Title like '%" + SearchModel.Title + "%'";
            }
            if (SearchModel.Type.HasValue)
            {
                strWhere += " and TypeId=" + (int)SearchModel.Type.Value + "";
            }
            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_Adv> q = rdc.ExecuteQuery<tbl_Adv>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tbl_Adv GetModel(int id)
        {
            using (FWDC rdc=new FWDC())
            {
                var model = rdc.tbl_Adv.FirstOrDefault(n => n.ID == id);
                return model;
            }
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="model">tbl_Adv 实体</param>
        /// <returns></returns>
        public static bool Add(tbl_Adv model)
        {
            using (FWDC rdc=new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Adv.InsertOnSubmit(model);
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
        /// 修改广告
        /// </summary>
        /// <param name="model">tbl_Adv实体</param>
        /// <returns></returns>
        public static bool Update(tbl_Adv model)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_Adv.FirstOrDefault(n => n.ID == model.ID);
                if (m != null)
                {
                    m.Title = model.Title;
                    m.TypeId = model.TypeId;
                    m.PhotoPath = model.PhotoPath;
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
        /// 删除广告
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Delete(int Id)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_Adv.FirstOrDefault(n => n.ID == Id);
                if (m != null)
                {
                    rdc.tbl_Adv.DeleteOnSubmit(m);
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
