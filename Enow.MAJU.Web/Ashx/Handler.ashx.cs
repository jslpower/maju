using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace CordovaService
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {
        HttpContext context = null;
        /// <summary>
        /// 文件保存路径
        /// </summary>
        string UploadFilepath = "/ufiles/";

        public void ProcessRequest(HttpContext _context)
        {
            context = _context;
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";


            string dotype = context.Request.QueryString["dotype"];

            switch (dotype)
            {
                case "shangchuan": ShangChuan(); break;
                default: UnHandler(); break;
            }            
        }

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region private members
        /// <summary>
        /// rcwe
        /// </summary>
        /// <param name="s"></param>
        void RCWE(string s)
        {
            context.Response.Clear();
            context.Response.Write(s);
            context.Response.End();
        }

        /// <summary>
        /// un handler
        /// </summary>
        void UnHandler()
        {
            RCWE("UNHANDLER");
        }

        /// <summary>
        /// 上传
        /// </summary>
        void ShangChuan()
        {
            HttpPostedFile file = context.Request.Files["file"];

            if (file == null)
            {
                RCWE("-1");
            }

            var info = upload(file);

            if (info == null)
            {
                RCWE("-1");
            }

            RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(info));
        }

        /// <summary>
        /// upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        MFileInfo upload(HttpPostedFile file)
        {
            var info = new MFileInfo();

            info.FileName = file.FileName;
            string fileentry_fullpath = context.Request.Form["fileentry_fullpath"];
            string fileentry_url = context.Request.Form["fileentry_url"];

            if (file.ContentLength < 1 || string.IsNullOrEmpty(info.FileName))
            {
                info.RetCode = "-1";
                info.XiaoXi = "上传文件错误";

                return info;
            }

            if (file.ContentLength > 2097152)
            {
                info.RetCode = "-1";
                info.XiaoXi = "上传文件错误(文件大小不能超过2M)";

                return info;
            }

            string filePath = string.Empty;
            //获取文件扩展名
            string fielExtension = Path.GetExtension(fileentry_url);

            //设置文件名
            Random rnd = new Random();
            string saveFileName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + fielExtension;
            rnd = null;

            //保存文件
            string dPath = System.Web.HttpContext.Current.Server.MapPath(UploadFilepath);

            if (!Directory.Exists(dPath)) Directory.CreateDirectory(dPath);

            string fPath = dPath + saveFileName;
            file.SaveAs(fPath);

            filePath = UploadFilepath + saveFileName;

            info.RetCode = "0";
            info.Filepath = filePath;

            return info;
        }
        #endregion

        #region MFileInfo
        /// <summary>
        /// MFileInfo
        /// </summary>
        public class MFileInfo
        {
            string _RetCode = "0";
            /// <summary>
            /// 上传结果，0:成功，其它失败
            /// </summary>
            public string RetCode { get { return _RetCode; } set { _RetCode = value; } }
            /// <summary>
            /// 上传消息
            /// </summary>
            public string XiaoXi { get; set; }
            /// <summary>
            /// 上传的文件完全限定名称
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// 文件路径
            /// </summary>
            public string Filepath { get; set; }
            /// <summary>
            /// 文件扩展名
            /// </summary>
            public string FileExtension
            {
                get
                {
                    if (string.IsNullOrEmpty(Filepath)) return string.Empty;
                    return Path.GetExtension(Filepath);
                }
            }
        }
        #endregion
    }
}
