using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.MAJU.Utility;

namespace Enow.MAJU.WebApp.ashx
{
    /// <summary>
    /// uploadImgCode 的摘要说明
    /// </summary>
    public class uploadImgCode : IHttpHandler
    {
        /// <summary>
        /// 文件保存路径
        /// </summary>
        string UploadFilepath = "/ufiles/";

        public void ProcessRequest(HttpContext context)
        {
            string img;//接收经过base64编 之后的字符串   
            context.Response.ContentType = "text/plain";
            try
            {
                img = context.Request["img"].ToString();
                //获取base64字符串
                img = img.Split(',')[1];
                byte[] imgBytes = Convert.FromBase64String(img);
                //将base64字符串转换为字节数组            
                System.IO.Stream stream = new System.IO.MemoryStream(imgBytes);
                //将字节数组转换为字节流      
                //将流转回Image，用于将PNG 式照片转为jpg，压缩体积以便保存。         
                System.Drawing.Image imgae = System.Drawing.Image.FromStream(stream);
                var filename = Guid.NewGuid().ToString() + ".jpg";
                imgae.Save(context.Server.MapPath("/ufiles/") + filename, System.Drawing.Imaging.ImageFormat.Jpeg);//保存图片            
                context.Response.Write(UtilsCommons.AjaxReturnJson("1", "上传成功", UploadFilepath + filename));//输出调用结果               
            }
            catch (Exception msg)
            {
                img = null;
                context.Response.Write(UtilsCommons.AjaxReturnJson("-1", msg.Message));
                return;
            }
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