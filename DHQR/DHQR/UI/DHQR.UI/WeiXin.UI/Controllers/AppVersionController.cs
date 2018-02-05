using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;
using DHQR.DataAccess.Entities;
using System.IO;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// APP版本信息控制层
    /// </summary>
    public class AppVersionController : BaseController
    {
        //
        // GET: /AppVersion/


        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new AppVersionModelService();
            PagedResults<AppVersionModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id)
        {
            var service = new AppVersionModelService();
            AppVersionModel model = new AppVersionModel();
            if (id.HasValue)
            {
                model = service.GetByKey(id.Value);
            }
            
            return View(model);
        }




        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(AppVersionModel model)
        {
            var service = new AppVersionModelService();
            DoHandle doHandle;
            service.Add(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(AppVersionModel model)
        {
            var service = new AppVersionModelService();
            DoHandle doHandle;
            service.Edit(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(AppVersionModel model)
        {
            var service = new AppVersionModelService();
            DoHandle doHandle;
            service.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        //用户上传【图文消息封面】
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        [HttpPost]
        public JsonResult UpLoadImage()
        {
            var postedFile = Request.Files[0];

            if (postedFile == null)
            {
                Response.StatusCode = 404;
                Response.Write("Not Found");
                Response.End();
                return null;
            }
            string fileName = Path.GetFileName(postedFile.FileName);
            string filePath = Server.MapPath("/fileupload/" + fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            postedFile.SaveAs(filePath);
           var kk=  Request.ServerVariables["LOCAl_ADDR"];
           var port = Request.ServerVariables["Server_Port"].ToString();
          //  var kk = "http://" + HttpContext.Server.
           var url = kk+":"+port + "/fileupload/" + fileName;
           return Json(url, "text/html");
        }


    }
}
