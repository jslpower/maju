using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using System.Web;
using Enow.MAJU.Utility;

namespace Enow.MAJU.BLL
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<ManagerList> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, string UserName, string KeyWord)
        {
            string FieldsList = "*";
            string TableName = "tbl_ManagerList";
            string OrderString = " ORDER BY Id ASC";
            string strWhere = "(1=1)";
            if (UserName != null)
            {
                strWhere = strWhere + " AND UserName like '%" + UserName + "%'";
            }
            if (KeyWord != null)
            {
                strWhere = strWhere + " AND (ContactName like '%" + KeyWord + "%')";
            }

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<ManagerList> q = rdc.ExecuteQuery<ManagerList>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 取得用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static ManagerList GetModel(int Id)
        { 
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.ManagerList.FirstOrDefault(n=>n.Id==Id);
                return model;
            }
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.ManagerList.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public static bool IsExistsUserName(string user_name)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.UserName == user_name);
                if (m != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!String.IsNullOrEmpty(model.Password))
                        m.Password = model.Password;
                    m.RoleId = model.RoleId;
                    m.ContactTel = model.ContactTel;
                    m.PermissionList = model.PermissionList;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateContactInfo(string UserName,string ContactName)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.UserName == UserName);
                if (m != null)
                {
                    m.ContactName = ContactName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdatePassword(int Id,string Password)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (!String.IsNullOrEmpty(Password))
                        m.Password = Password;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 用户独立授权
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdatePer(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.PermissionList = model.PermissionList;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 用户独立授权
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateQuickMenu(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.QuickMenu = model.QuickMenu;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(int EmployeeId)
        {
            using (FWDC rdc = new FWDC())
            {                
                var m = rdc.ManagerList.FirstOrDefault(n => n.EmployeeId == EmployeeId);
                if (m != null)
                {
                    rdc.ManagerList.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Enable(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsEnable = true;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Disabled(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsEnable = false;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}