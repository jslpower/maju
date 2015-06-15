using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.MAJU.Utility;
using System.Text;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 国家省份城市区县
    /// </summary>
    public class GetWxProvinceAndCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");
            StringBuilder sb = new StringBuilder();
          
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));//省份ID
         
            switch (getType)
            {
               
                case "p":

                    IList<Enow.MAJU.BLL.tbl_SysProvince> pList = Enow.MAJU.BLL.BMSysProvince.GetWxProvinceList(new MAJU.Model.MProvinceSearch { CountryId=1 });

                    if (pList != null && pList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < pList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + pList[i].ProvinceId.ToString() + "\",\"name\":\"" + pList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }

                    break;
                case "c":

                    IList<Enow.MAJU.BLL.tbl_SysCity> cList = Enow.MAJU.BLL.BMSysProvince.GetWxCityList(new MAJU.Model.MCitySearch { ProvinceId = pID });
                    if (cList != null && cList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < cList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + cList[i].CityId.ToString() + "\",\"name\":\"" + cList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;

                default: break;
            }

            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}