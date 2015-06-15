using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
    /// <summary>
    /// 会员
    /// </summary>
    public class BMember
    {
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_Member> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.MemeberSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " tbl_Member ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (SearchModel.ProvinceId.HasValue)
            {
                strWhere += " and ProvinceId=" + SearchModel.ProvinceId + "";
            }
            if (SearchModel.CityId.HasValue)
            {
                strWhere += " and CityId=" + SearchModel.CityId + "";
            }
            if (SearchModel.AreaId.HasValue)
            {
                strWhere += " and AreaId=" + SearchModel.AreaId + "";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberName))
            {
                strWhere += " and MemberName like '%" + SearchModel.MemberName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.NickName))
            {
                strWhere += " and NickName like '%" + SearchModel.NickName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.Mobile))
            {
                strWhere += " and mobile like '%" + SearchModel.Mobile + "%'";
            }
            if (SearchModel.Status.HasValue)
            {
                strWhere += " and status =" + (int)SearchModel.Status + "";
            }
            if (SearchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + SearchModel.IssueBeginTime.Value + "'";
            }
            if (SearchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + SearchModel.IssueEndTime.Value.AddDays(1) + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<tbl_Member> q = rdc.ExecuteQuery<tbl_Member>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Member> GetMemberList()
        {
            using (FWDC rdc = new FWDC())
            {
                List<tbl_Member> q = rdc.ExecuteQuery<tbl_Member>(@"SELECT * FROM tbl_Member where status=1 ").ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <param name="Id">MemberId</param>
        /// <returns></returns>
        public static tbl_Member GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == Id);
                return model;
            }
        }

        /// <summary>
        /// 根据手机号码取得用户实体
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static tbl_Member GetModelByMobile(string mobile)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Member.FirstOrDefault(n => n.Mobile == mobile);
                return model;
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_Member model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Member.InsertOnSubmit(model);
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
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_Member model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == model.MemberId);
                if (m != null)
                {
                    m.CountryId = model.CountryId;
                    m.CountryName = model.CountryName;
                    m.ProvinceId = model.ProvinceId;
                    m.ProvinceName = model.ProvinceName;
                    m.CityId = model.CityId;
                    m.CityName = model.CityName;
                    m.AreaId = model.AreaId;
                    m.AreaName = model.AreaName;
                    if (!String.IsNullOrWhiteSpace(model.NickName))
                    {
                        m.NickName = model.NickName;
                    }
                    m.HeadPhoto = model.HeadPhoto;
                    m.Gender = model.Gender;
                    m.Age = model.Age;
                    m.Revenue = model.Revenue;
                    m.Requirement = model.Requirement;
                    m.Email = model.Email;
                    if (!string.IsNullOrWhiteSpace(model.Mobile))
                    {

                        m.Mobile = model.Mobile;
                    }
                    if (!string.IsNullOrWhiteSpace(model.MemberName))
                    {

                        m.MemberName = model.MemberName;
                    }
                    if (!String.IsNullOrWhiteSpace(model.Password))
                    {
                        m.Password = model.Password;
                    }
                    m.LastUpdateTime = DateTime.Now;

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
        /// 修改用户密码
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool ChangePassword(string MemberId, string Password)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    m.Password = Password;
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
        /// 判断手机号码是否存在
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>存在：true  不存在：false</returns>
        public static bool IsExistsMobile(string mobile)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Mobile == mobile);
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

        /// <summary>
        /// 判断手机号码是否存在
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>存在：true  不存在：false</returns>
        public static bool IsExistsMobile(string mobile, out string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Mobile == mobile);
                if (m != null)
                {
                    memberid = m.MemberId;
                    return true;
                }
                else
                {
                    memberid = string.Empty;
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool UpdateStatus(string MemberId, Model.EnumType.用户状态 Status)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    m.Status = (Byte)Status;
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
        /// 修改用户推送
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="ispush"></param>
        /// <returns></returns>
        public static bool UpdatePush(string MemberId, bool ispush)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    m.IsPush = char.Parse(ispush ? "1" : "0");
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
        /// 保存百度推送信息
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="userid"></param>
        /// <param name="channelid"></param>
        /// <returns></returns>
        public static bool UpdateBaiDuInfo(string MemberId, string userid,string channelid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    m.userid = userid;
                    m.channelid = channelid;
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
        /// 判断QQ号码是否已注册
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static bool IsExistsQQ(string OpenId)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.qqsocialid == OpenId);
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

        /// <summary>
        /// 判断微博账号是否已注册
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static bool IsExistsWeiBo(string OpenId)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.weibosocialid == OpenId);
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

        #region 取得用户昵称
        /// <summary>
        /// 当用户昵称为空的时候，返回用户的手机号码
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static string GetUserName(string MemberId)
        {
            string retValue = "";
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.NickName))
                {
                    retValue = model.NickName;
                }
                else if (!string.IsNullOrWhiteSpace(model.Mobile) && model.Mobile.Length == 11)
                {
                    retValue = model.Mobile.Replace(model.Mobile.Substring(3, 4), "****");
                }
                else
                {
                    retValue = model.NickName;
                }
            }
            return retValue;
        }
        #endregion
    }

    /// <summary>
    /// 会员收藏
    /// </summary>
    public class BMemberCollect
    {
        /// <summary>
        /// 收藏分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<MemberCollect> GetList(ref int RowsCount, int PageSize, int PageIndex, Model.CollectSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " tbl_MemberCollect ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere += " and MemberId='" + SearchModel.MemberId + "'";
            }
            if (SearchModel.RelatedType.HasValue)
            {
                strWhere += " and RelatedType=" + (int)SearchModel.RelatedType + "";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.RelatedId))
            {
                strWhere += " and RelatedId='" + SearchModel.RelatedId + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<MemberCollect> q = rdc.ExecuteQuery<MemberCollect>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 收藏分页列表
        /// </summary>
        /// <param name="RowsCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<dt_MemberCollect> GetViewList(ref int RowsCount, int PageSize, int PageIndex, Model.CollectSearch SearchModel)
        {
            string FieldList = " * ";
            string TableName = " dt_MemberCollect ";
            string OrderString = " order by IssueTime desc";
            string strWhere = " 1=1";
            if (!string.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere += " and MemberId='" + SearchModel.MemberId + "'";
            }
            if (SearchModel.RelatedType.HasValue)
            {
                strWhere += " and RelatedType=" + (int)SearchModel.RelatedType + "";
            }
            if (!string.IsNullOrWhiteSpace(SearchModel.RelatedId))
            {
                strWhere += " and RelatedId='" + SearchModel.RelatedId + "'";
            }

            int PageLowerBound = (PageIndex - 1) * PageSize + 1;
            int PageUpperBound = PageLowerBound + PageSize - 1;
            int skipRows = (PageIndex - 1) * PageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                RowsCount = query.First<int>();
                List<dt_MemberCollect> q = rdc.ExecuteQuery<dt_MemberCollect>(@"SELECT " + FieldList + " FROM (SELECT " + FieldList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(MemberCollect model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.MemberCollect.InsertOnSubmit(model);
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
        /// 判断是否已收藏
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="RelatedId"></param>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static bool IsExists(int TypeId, string RelatedId, string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.MemberCollect.FirstOrDefault(n => n.RelatedType == TypeId && n.RelatedId == RelatedId && n.MemberId == MemberId);
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

        /// <summary>
        /// 根据收藏ID取得会员收藏实体
        /// </summary>
        /// <param name="CollectId"></param>
        /// <returns></returns>
        public static MemberCollect GetModel(string CollectId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.MemberCollect.FirstOrDefault(n => n.CollectId == CollectId);
                return model;
            }
        }
        
        /// <summary>
        /// 更新收藏数
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UpdateType"></param>
        /// <param name="OperatorType"></param>
        /// <returns></returns>
        public static bool UpdateCollects(string Id, Model.EnumType.收藏类别 UpdateType, Model.EnumType.操作符号 OperatorType)
        {
            var isok = false;
            using (FWDC rdc = new FWDC())
            {
                switch (UpdateType)
                {
                    case Model.EnumType.收藏类别.保险:
                        var p = rdc.tbl_Product.FirstOrDefault(m => m.ProductId == Id);
                        if (p != null)
                        {
                            switch (OperatorType)
                            {
                                case Model.EnumType.操作符号.加:
                                    p.Collects = p.Collects + 1;
                                    break;
                                case Model.EnumType.操作符号.减:
                                    p.Collects = p.Collects - 1;
                                    break;
                                default:
                                    break;
                            }
                            rdc.SubmitChanges();
                            isok = true;
                        }
                        break;
                    case Model.EnumType.收藏类别.话题:
                        var t = rdc.tbl_MemberTopic.FirstOrDefault(m => m.TopicId == Id);
                        if (t != null)
                        {
                            switch (OperatorType)
                            {
                                case Model.EnumType.操作符号.加:
                                    t.Collects = t.Collects + 1;
                                    break;
                                case Model.EnumType.操作符号.减:
                                    t.Collects = t.Collects - 1;
                                    break;
                                default:
                                    break;
                            }
                            rdc.SubmitChanges();
                            isok = true;
                        }
                        break;
                    case Model.EnumType.收藏类别.资讯:
                        var n = rdc.tbl_News.FirstOrDefault(m => m.NewsId == Id);
                        if (n != null)
                        {
                            switch (OperatorType)
                            {
                                case Model.EnumType.操作符号.加:
                                    n.Collects = n.Collects + 1;
                                    break;
                                case Model.EnumType.操作符号.减:
                                    n.Collects = n.Collects - 1;
                                    break;
                                default:
                                    break;
                            }
                            rdc.SubmitChanges();
                            isok = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return isok;
        }
    }


}
