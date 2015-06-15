using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;

namespace Enow.MAJU.WebApp
{
    public partial class about : BackPage
    {
        protected int RowsCount = 0;

        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCheck();
            if (!IsPostBack)
            {
                var list = BAdv.GetList(ref RowsCount, 1, 1, new Model.AdvSearch() { Type = Model.EnumType.导航条位置.关于马驹 });
                if (list!=null&&list.Count>0)
                {
                    ltrimg.Text="<img src="+list[0].PhotoPath.ToString()+">";
                }
                else
                {
                ltrimg.Text="<img src=\"images/about.png\">";
                }
            }
        }
    }
}