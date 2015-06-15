using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.Model;
using Enow.MAJU.BLL;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Adv
{
    public partial class Service : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            var list = BService.GetList();
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                phNoData.Visible = true;
            }
        }
    }
}