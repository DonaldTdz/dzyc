using Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.DataAccess.Entities;
using DHQR.UI.Models;

using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 图文消息抬头
    /// </summary>
    public class WeiXinPicMsgMatserController : WeiXinBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id,int? type) 
        {
            WeiXinPicMsgMatserModelService masterModelSer=new WeiXinPicMsgMatserModelService();
            var urlData = masterModelSer.GetUrlSelectList();
            urlData.Insert(0, new SelectListItem() { Text = "请选择", Value = "",Selected=true });
            ViewData["url"] = urlData;
            WeiXinPicMsgMatserModel model = new WeiXinPicMsgMatserModel();
            if (id.HasValue)
            {
                model = masterModelSer.GetDataById(id.Value);
            }
            else {
                model.CreateTime = DateTime.Now;
                model.MaterialType = type.Value;
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult Query() 
        {
            var result = new WeiXinPicMsgMatserModelService().GetDataByAppId(wechat_id);
            return Json(result);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="detailsStr"></param>
        /// <param name="master"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult SaveData(string detailsStr, WeiXinPicMsgMatser master) 
        {
            master.WeiXinAppId = wechat_id;
            master.CreateTime = DateTime.Now;
            master.Creator = user_name;
            var dohandle = new WeiXinPicMsgMatserModelService().SaveData(detailsStr, master);
            
            return Json(dohandle);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult DeleteData(Guid id) 
        {
            DoHandle dohandle = new WeiXinPicMsgMatserModelService().DeleteData(id);
            return JsonForDoHandle(dohandle);
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
            string filePath = Server.MapPath("/images/fileupload/" + fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            postedFile.SaveAs(filePath);
            return Json("../images/fileupload/" + fileName, "text/html");
        }



    }
}
