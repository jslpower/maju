using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Message
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        protected int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        protected int PageIndex = UtilsCommons.GetPadingIndex(), RecordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }
        }

        private void InitList()
        {
            
        }
    }
}