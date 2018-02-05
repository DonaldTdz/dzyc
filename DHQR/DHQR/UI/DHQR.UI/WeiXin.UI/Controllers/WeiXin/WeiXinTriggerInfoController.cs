using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHQR.UI.Controllers
{
    public class WeiXinTriggerInfoController : Controller
    {
        public static readonly string converTmpPath = "../UpLoadFiles/Covers/tmp/";
        public static readonly string converFinalPath = "../UpLoadFiles/Covers/final/";

        public ActionResult Index()
        {
            return View();
        }

        //用户上传【图文消息封面】
        public JsonResult UpLoadImage()
        {
            var postedFile = Request.Files["Filedata"];
            if (postedFile == null)
            {
                //Response.StatusCode = 404;
                //Response.Write("Not Found");
                //Response.End();
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
            }
            string dirPath = HttpContext.Server.MapPath(converTmpPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            int index = postedFile.FileName.IndexOf(".");
            string fileExt = postedFile.FileName.Substring(index + 1);
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");
            string filePath = string.Format("{0}\\{1}.{2}", dirPath, fileName, fileExt);
            postedFile.SaveAs(filePath);

            string response = string.Format("{0}{1}.{2}", converTmpPath, fileName, fileExt);
            return Json(new { filepath = response, status = 1 }, JsonRequestBehavior.AllowGet);
            //Response.Write(response);
            //Response.End();
        }

    }
}
