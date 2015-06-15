using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;

namespace Enow.MAJU.Web
{
    public partial class UserLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageUserAuth.Logout("/");
        }
    }
}