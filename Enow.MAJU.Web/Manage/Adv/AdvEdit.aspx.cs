using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Adv
{
    public partial class AdvEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            string dotype = Utils.GetQueryStringValue("dotype");
            switch (dotype)
            {
                case "save":
                    SaveData();
                    break;
                default:
                    break;
            }

            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                InitType();
                string act = Utils.GetQueryStringValue("act");
                switch (act)
                {
                    case "update":
                        InitPage();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 加载广告管理
        /// </summary>
        private void InitPage()
        {
            int Id = Utils.GetInt(Utils.GetQueryStringValue("Id"), 0);
            if (Id > 0)
            {
                var model = BAdv.GetModel(Id);
                if (model != null)
                {
                    txtTitle.Text = model.Title;

                    if (model.TypeId > 0)
                    {
                        ddlType.Items.FindByValue(model.TypeId.ToString()).Selected = true;
                    }
                    txtSortId.Text = model.SortId.ToString();
                    #region 广告图片
                    IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                    files.Add(new Model.MFileInfo() { FilePath = model.PhotoPath });
                    UploadControl1.YuanFiles = files;
                    #endregion
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未找到您要查看的信息"));
                return;
            }
        }

        #region 绑定广告位置
        /// <summary>
        /// 绑定广告位置
        /// </summary>
        private void InitType()
        {
            Array array = Enum.GetValues(typeof(Model.EnumType.导航条位置));
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.EnumType.导航条位置), arr.ToString());
                string text = Enum.GetName(typeof(Model.EnumType.导航条位置), arr);
                this.ddlType.Items.Add(new ListItem() { Text = text, Value = value.ToString() });
            }
            if ( Utils.GetQueryStringValue("act")=="add")
            {
             
            this.ddlType.Items[0].Selected = true;   
            }
        }
        #endregion

        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadControl1.Files;
            var files2 = UploadControl1.YuanFiles;
            if (files1 != null && files1.Count > 0)
            {
                return files1[0].FilePath;
            }
            if (files2 != null && files2.Count > 0)
            {
                return files2[0].FilePath;
            }
            return string.Empty;
        }


        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            #region 取值
            bool IsRet = false;
            string strErr = "";
            string act = Utils.GetQueryStringValue("act");
            int Id = Utils.GetInt(Utils.GetQueryStringValue("Id"), 0);
            string Title = Utils.GetFormValue(txtTitle.UniqueID);
            int TypeId = Utils.GetInt(Utils.GetFormValue(ddlType.UniqueID), 0);
            int SortId = Utils.GetInt(Utils.GetFormValue(txtSortId.UniqueID), 0);
            string PhotoPath = GetAttachFile();
            #endregion

            #region 判断
            if (String.IsNullOrWhiteSpace(Title))
            {
                strErr += "请填写广告标题！";
            }
            if (String.IsNullOrWhiteSpace(PhotoPath))
            {
                strErr += "请上传广告图片!";
            }
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                return;
            }
            #endregion

            switch (act)
            {
                case "add":
                    IsRet = BAdv.Add(new tbl_Adv
                    {
                        Title = Title,
                        TypeId = TypeId,
                        SortId = SortId,
                        PhotoPath = PhotoPath
                    });
                    break;

                case "update":
                    IsRet = BAdv.Update(new tbl_Adv
                    {
                        ID=Id,
                        Title = Title,
                        TypeId = TypeId,
                        SortId = SortId,
                        PhotoPath = PhotoPath
                    });
                    break;
                default:
                    break;
            }
            if (IsRet)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));

            }
        }
    }


}