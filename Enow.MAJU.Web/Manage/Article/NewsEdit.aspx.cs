using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.MAJU.BLL;
using Enow.MAJU.Model;
using Enow.MAJU.Utility;

namespace Enow.MAJU.Web.Manage.Article
{
    public partial class NewsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("doType").ToLower();
            if (dotype == "save")
            {
                SaveData();
                return;
            }

            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                string act = Utils.GetQueryStringValue("act");
                switch (act)
                {
                    case "update":
                        InitPage(Id);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitPage(string Id)
        {
            var model = BNews.GetModel(Id);
            if (model != null)
            {
                txtTitle.Text = model.Title;
                #region 资讯图片
                //IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                //files.Add(new Model.MFileInfo() { FilePath = model.PhotoPath });
                //UploadControl1.YuanFiles = files;
                #endregion
                ddlHot.Items.FindByValue(model.IsHot.ToString()).Selected = true;
                ddlTop.Items.FindByValue(model.IsTop.ToString()).Selected = true;
             
                txtContent.Text = model.Context;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要查看的信息！"));
                return;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveData()
        {
            #region 取值
            bool IsResult = false;
            string strErr = "";
            ManagerList manageModel = ManageUserAuth.GetManageUserModel();
            int OperatorId = manageModel.Id;
            string OperatorName = manageModel.ContactName;
            string act = Utils.GetQueryStringValue("act").ToLower();
            string Id = Utils.GetQueryStringValue("Id");
            string Title = Utils.GetFormValue(txtTitle.UniqueID);
           // string PhotoPath = GetAttachFile();
            string PhotoPath = "";
            char IsTop = char.Parse(Utils.GetFormValue(ddlTop.UniqueID));
            char IsHot = char.Parse(Utils.GetFormValue(ddlHot.UniqueID));
            char IsEssence ='0';
            string Context = Utils.GetFormEditorValue(txtContent.UniqueID);
            #endregion

            #region 判断

            if (String.IsNullOrWhiteSpace(Title))
            {
                strErr = "请填写资讯标题!";
            }
           
            if (String.IsNullOrWhiteSpace(Context))
            {
                strErr += "请填写资讯正文!";
            }
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                return;
            }
            #endregion

            #region 保存数据
            switch (act)
            {
                case "add":
                    IsResult = BNews.Add(new tbl_News
                    {
                        NewsId = Guid.NewGuid().ToString(),
                        Title = Title,
                        PhotoPath = PhotoPath,
                        Context = Context,
                        Replys = 0,
                        Clicks = 0,
                        Collects = 0,
                        IsHot = IsHot,
                        IsTop = IsTop,
                        IsEssence=IsEssence,
                        state = '0',
                        OperatorId = OperatorId,
                        OperatorName = OperatorName,
                        IssueTime = DateTime.Now
                    });
                    break;

                case "update":
                    IsResult = BNews.Update(new tbl_News
                    {
                        NewsId = Id,
                        Title = Title,
                        PhotoPath = PhotoPath,
                        Context = Context,

                        IsHot = IsHot,
                        IsTop = IsTop,
                        IsEssence=IsEssence,

                        OperatorId = OperatorId,
                        OperatorName = OperatorName

                    });
                    break;

                default:
                    IsResult = false;
                    break;
            }
            if (IsResult)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));

            }
            #endregion
        }

        #region
        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        /*
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
        }*/
        #endregion
    }
}