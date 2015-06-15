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
    public partial class Collect : BackPage
    {
        protected int pagesize = 20;
        protected int pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int rowscount = 0;
        protected Model.EnumType.收藏类别 T = (Model.EnumType.收藏类别)Utils.GetInt(Utils.GetQueryStringValue("t"));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "getdatas") Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(GetList()));
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        List<dt_MemberCollect> GetList()
        {
            return BMemberCollect.GetViewList(ref rowscount, pagesize, pageindex, new Model.CollectSearch() { MemberId = GetUserModel().MemberId, RelatedType = T });
        }
        void InitPage()
        {
            var l = GetList();
            
            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();
            }
        }
    }
}