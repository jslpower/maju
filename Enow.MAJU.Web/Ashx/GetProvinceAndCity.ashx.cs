using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.MAJU.Utility;
using System.Text;

namespace Enow.MAJU.Web.Ashx
{
    /// <summary>
    /// GetProvinceAndCity 的摘要说明
    /// </summary>
    public class GetProvinceAndCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");
            StringBuilder sb = new StringBuilder();
            int gID = Utils.GetInt(Utils.GetQueryStringValue("gid"));//国家ID
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));//省份ID
            int cID = Utils.GetInt(Utils.GetQueryStringValue("cid"));//城市ID
            int xID = Utils.GetInt(Utils.GetQueryStringValue("xid"));//区县ID
            switch (getType)
            {
                case "g":

                    IList<Enow.MAJU.BLL.tbl_SysCountry> gList = Enow.MAJU.BLL.BMSysProvince.GetCountryList();
                    if (gList != null && gList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < gList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + gList[i].CountryId.ToString() + "\",\"name\":\"" + gList[i].Name + "\"},");
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
                case "p":

                    IList<Enow.MAJU.BLL.tbl_SysProvince> pList = Enow.MAJU.BLL.BMSysProvince.GetProvinceList(new Enow.MAJU.Model.MProvinceSearch { CountryId = gID });

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

                    IList<Enow.MAJU.BLL.tbl_SysCity> cList = Enow.MAJU.BLL.BMSysProvince.GetCityList(new Enow.MAJU.Model.MCitySearch { ProvinceId = pID });
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

                case "x":
                    IList<Enow.MAJU.BLL.tbl_SysArea> xList = Enow.MAJU.BLL.BMSysProvince.GetAreaList(new Enow.MAJU.Model.MAreaSearch { CityId = cID });
                    if (xList != null && xList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < xList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + xList[i].CountyId.ToString() + "\",\"name\":\"" + xList[i].Name + "\"},");
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