using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp
{
    public partial class index : BackPage
    {
        protected int pagesize = 10;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"));
        protected int rowscount = 0;
        protected tbl_Member UserInfo = null;

        protected int IsPush = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "gethot") GetHot();
            if (Utils.GetQueryStringValue("dotype") == "getessence") GetEssence();
            if (Utils.GetQueryStringValue("dotype") == "save") Save();
            if (!IsPostBack)
            {
                InitData();
            }
            if (IsLoginCheck())
            {
                UserInfo = GetUserModel();
                IsPush =int.Parse(UserInfo.IsPush.ToString());
            }
        }
        void InitData()
        {
            var d = BMemberTopic.GetTops(ref rowscount, pagesize, 1, new Model.TopSearch() { IsHot = Model.EnumType.是否热门.是 });
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }
        }
        void Save()
        {
            if (!IsLoginCheck())
            {
                var r = BMember.UpdateBaiDuInfo(UserInfo.MemberId, Utils.GetQueryStringValue("userid"), Utils.GetQueryStringValue("channelid"));
                Utils.RCWE(UtilsCommons.AjaxReturnJson(r ? "1" : "0", r ? "保存成功" : "保存失败"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1",""));
            }
           
        }
        void GetHot()
        {
            var d = BMemberTopic.GetTops(ref rowscount, pagesize, pageindex, new Model.TopSearch() { IsHot = Model.EnumType.是否热门.是 });
            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(d));
        }
        void GetEssence()
        {
            var d = BMemberTopic.GetTops(ref rowscount, pagesize, pageindex, new Model.TopSearch() { IsEssence = true });
            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(d));
        }
    }
}