using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//客户服务
namespace Enow.MAJU.BLL
{
   public class BService
    {
       /// <summary>
       /// 获取客户服务信息集合
       /// </summary>
       /// <returns></returns>
       public static List<tbl_ServiceInfo> GetList()
       {
           using (FWDC rdc=new FWDC())
           {
               List<tbl_ServiceInfo> q = rdc.ExecuteQuery<tbl_ServiceInfo>(@" select * from tbl_ServiceInfo").ToList();
               return q;
           }    
       }

       /// <summary>
       /// 获取客户服务实体
       /// </summary>
       /// <returns></returns>
       public static tbl_ServiceInfo GetModel()
       {
           using (FWDC rdc=new FWDC())
           {
               var model = rdc.tbl_ServiceInfo.FirstOrDefault(n => n.ID == n.ID);
               return model;
           }
       }

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool Add(tbl_ServiceInfo model)
       {
           using (FWDC rdc=new FWDC())
           {
               if (model != null)
               {
                   rdc.tbl_ServiceInfo.InsertOnSubmit(model);
                   rdc.SubmitChanges();
                   return true;
               }
               else
               {
                   return false;
               }
           }
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="Model"></param>
       /// <returns></returns>
       public static bool Update(tbl_ServiceInfo Model)
       {
           using (FWDC rdc=new FWDC())
           {
               var m = rdc.tbl_ServiceInfo.FirstOrDefault(n => n.ID == Model.ID);
               if (m != null)
               {
                   m.WeChat = Model.WeChat;
                   m.QQ = Model.QQ;
                   m.Weibo = Model.Weibo;
                   m.Email = Model.Email;
                   rdc.SubmitChanges();
                   return true;
               }
               else
               {
                   return false;
               }
           }
       }
    }
}
