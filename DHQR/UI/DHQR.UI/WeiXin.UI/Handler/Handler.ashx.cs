using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.BarCode;
using System.Drawing;

namespace Basic.UI.NewFolder1
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {
        //"Handler.ashx?OrderNo=CH 32134411"; 
        //获取Code39处理程序 
        public void ProcessRequest(HttpContext context)
        {
            string OrderNo = context.Request.Params["OrderNo"];
            Common.UI.BarCode.Code39Helper code39Helper = new Code39Helper();
            code39Helper.Height = 60;
            code39Helper.Magnify = 0;
            code39Helper.ViewFont = new Font("Arial", 12);
            System.Drawing.Image _CodeImage = code39Helper.GetCodeImage(OrderNo, Common.UI.BarCode.Code39Helper.Code39Model.Code39Normal, true);
            System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
            _CodeImage.Save(_Stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ContentType = "image/tiff";
            context.Response.Clear();
            context.Response.BufferOutput = true;
            context.Response.BinaryWrite(_Stream.GetBuffer());
            context.Response.Flush();
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