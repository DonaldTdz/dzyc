using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DHQR.UI.Handler
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadMasterHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string folder = context.Request["folder"];
            int index = folder.IndexOf("_");
            string detailIdStr = folder.Substring(index + 1);
            string uploadPath = AppDomain.CurrentDomain.BaseDirectory + "Document\\";

            DHQR.UI.Models.AttachmentInfoModelService modelService = new DHQR.UI.Models.AttachmentInfoModelService();
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string filePath = string.Empty; ;
                bool isSuccess = true;//modelService.AddAttachmentToProjectMaster(file.FileName, detailIdStr, out filePath);
                if (isSuccess)
                {
                    file.SaveAs(filePath);
                }
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
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