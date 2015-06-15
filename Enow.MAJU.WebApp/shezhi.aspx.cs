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
    public partial class shezhi : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "save") Save();
            this.@switch.Items.FindByValue(GetUserModel().IsPush.ToString()).Selected = true;
        }
        void Save()
        {
            var r = BMember.UpdatePush(GetUserModel().MemberId, Utils.GetFormValue(this.@switch.UniqueID) == "1");

            Utils.RCWE(UtilsCommons.AjaxReturnJson((r ? "1" : "0"), (r ? "保存成功" : "保存失败")));
        }
    }
}