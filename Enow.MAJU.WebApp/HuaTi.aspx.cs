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
    public partial class HuaTi : BackPage
    {
        protected int pagesize = 10;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (Utils.GetQueryStringValue("dotype") == "getmytopics") GetMyTopics();
            if (!IsPostBack)
            {
                InitData();
            }
        }
        void InitData()
        {
            var l = BMemberTopic.GetList(ref rowscount, pagesize, pageindex, new Model.TopicSearch() { State = Model.EnumType.状态.正常, MemberId = GetUserModel().MemberId, isView = true });
            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }
        void GetMyTopics()
        {
            var l = BMemberTopic.GetList(ref rowscount, pagesize, pageindex, new Model.TopicSearch() { State = Model.EnumType.状态.正常, MemberId = GetUserModel().MemberId, isView = true });
            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(l));
        }
    }
}