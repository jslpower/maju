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
    public partial class ziliao : BackPage
    {
        protected int ProvinceId = 0, CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                SaveData();
            }
            #endregion

            if (!IsPostBack)
            {
                InitData();
            }
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            #region 绑定年龄下拉框
            int Index = 1;
            while (Index <= 60)
            {
                selAge.Items.Add(new ListItem() { Value = Index.ToString(), Text = Index.ToString() });
                Index++;
            }
            selAge.DataBind();
            #endregion

            #region 绑定性别
            foreach (var item in Enum.GetValues(typeof(Model.EnumType.性别)))
            {
                selGender.Items.Add(new ListItem() { Value = ((int)item).ToString(), Text = Enum.GetName(typeof(Model.EnumType.性别), item) });
            }
            selGender.DataBind();

            #endregion

            #region 绑定家庭年收入
            foreach (var item in Enum.GetValues(typeof(Model.EnumType.家庭年收入)))
            {
                selRevenue.Items.Add(new ListItem() { Value = ((int)item).ToString(), Text = Enum.GetName(typeof(Model.EnumType.家庭年收入), item) });
            }
            selRevenue.DataBind();
            #endregion

            #region 绑定保险需求
            foreach (var item in Enum.GetValues(typeof(Model.EnumType.保险需求)))
            {
                selRequirement.Items.Add(new ListItem() { Value = ((int)item).ToString(), Text = Enum.GetName(typeof(Model.EnumType.保险需求), item) });
            }
            #endregion

            #region 初始化用户基础信息

            var model = GetUserModel();
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.HeadPhoto))
                {
                    imgHeadPhoto.Src = model.HeadPhoto;
                    hidHeadPhoto.Value = model.HeadPhoto;
                }
                txtNickName.Value = model.NickName;
                selGender.Items.FindByValue(model.Gender.ToString()).Selected = true;
                if (model.Age > 0)
                {

                    selAge.Items.FindByValue(model.Age.ToString()).Selected = true;
                }
                selRevenue.Items.FindByValue(model.Revenue.ToString()).Selected = true;

                ProvinceId = model.ProvinceId;
                CityId = model.CityId;
                if (!string.IsNullOrWhiteSpace(model.Requirement))
                {
                    //string[] arr = model.Requirement.Split(',');
                    //for (int i = 0; i < arr.Length; i++)
                    //{
                    //    selRequirement.Items.FindByValue(arr[i]).Selected = true;
                    //}
                    selRequirement.Items.FindByValue(model.Requirement.ToString()).Selected = true;
                }
                txtEmail.Value = model.Email;
                //txtRequirement.Value = model.Requirement;

            }

            #endregion
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            var model = GetUserModel();

            string HeadPhoto = Utils.GetFormValue(this.hidHeadPhoto.UniqueID);
            string nickName = Utils.GetFormValue(txtNickName.UniqueID);
            byte Gender = byte.Parse(Utils.GetFormValue(selGender.UniqueID));
            byte Age = byte.Parse(Utils.GetFormValue(selAge.UniqueID));
            byte Revenue = byte.Parse(Utils.GetFormValue(selRevenue.UniqueID));
            string Requirement = Utils.GetFormValue(selRequirement.UniqueID);
            string email = Utils.GetFormValue(txtEmail.UniqueID);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(selProvince.UniqueID));
            string ProvinceName = "";
            if (ProvinceId > 0)
            {
                var Pmodel = BMSysProvince.GetProvinceModel(ProvinceId);
                if (Pmodel != null)
                {
                    ProvinceName = Pmodel.Name;
                }
            }
            int CityId = Utils.GetInt(Utils.GetFormValue(selCity.UniqueID));
            string CityName = "";
            if (CityId > 0)
            {
                var Cmodel = BMSysProvince.GetCityModel(CityId);
                if (Cmodel != null)
                {
                    CityName = Cmodel.Name;
                }
            }

            bool ret = BMember.Update(new tbl_Member()
            {
                MemberId = model.MemberId,
                CountryId = 0,
                CountryName = "",
                ProvinceId = ProvinceId,
                ProvinceName = ProvinceName,
                CityId = CityId,
                CityName = CityName,
                AreaId = 0,
                AreaName = "",
                NickName = nickName,
                HeadPhoto = HeadPhoto,
                Gender = Gender,
                Age = Age,
                Revenue = Revenue,
                Requirement = Requirement,
                Email = email
            });
            WriteCookieInfo(BMember.GetModel(model.MemberId));
            Utils.RCWE(UtilsCommons.AjaxReturnJson(ret ? "1" : "0", ret ? "个人信息修改成功" : "个人信息修改失败"));

        }
    }
}