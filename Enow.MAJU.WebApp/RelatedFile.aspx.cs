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
    public partial class RelatedFile : BackPage
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "getdatas") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetDatas()));
            if (!IsPostBack)
            {
                InitData();
            }
        }
        List<dt_MemberTopic> GetDatas()
        {
            return BMemberTopic.GetList(ref rowscount, pagesize, pageindex, new Model.TopicSearch() { Title = Utils.GetQueryStringValue("title"), isView = true });
        }
        void InitData()
        {
            var d = GetDatas();
            if (d != null && d.Count > 0)
            {
                rpt.DataSource = d;
                rpt.DataBind();
            }
        }
    }
}