using Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class WeiXinArticleController : WeiXinBaseController
    {
        private readonly WeiXinArticleModelService modelSer = new WeiXinArticleModelService();
        
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
        /// <param name="param"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetPageData(QueryParam param) 
        {
            var result = modelSer.GetPageData(param);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult Del(WeiXinArticleModel model) 
        {
            DoHandle doHandle = new DoHandle();
            modelSer.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        #region 新增、编辑
        /// <summary>
        /// 新增或编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id) 
        {
            WeiXinArticleModel model = new WeiXinArticleModel();
            if (id.HasValue) {
                model = modelSer.GetByKey(id.Value);
                model.Content = model.Content.Replace("\"", "\\\"");
            }
            ViewData["ArticlesType"] = new WeiXinArticlesTypeModelService().GetArticlesType();
            return View(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        [ValidateInput(false)]
        public JsonResult Add(WeiXinArticleModel model) 
        {
            WeiXinSysUrlModelService urlService = new WeiXinSysUrlModelService();
            DoHandle doHandle = new DoHandle();
            model.WeiXinAppId = wechat_id;
            model.CreateTime = DateTime.Now;
            model.Id=Guid.NewGuid();
            modelSer.Add(model,out doHandle);
            var maxNum = urlService.GetAll().Max(f => f.Number);
            //添加系统URL
            WeiXinSysUrlModel urlModel = new WeiXinSysUrlModel 
            {
                Id=Guid.NewGuid(),
                Number = maxNum+1,
                Name=model.Title,
                Url="",
                WeiXinSysTypeId = Guid.Parse("D39AD09D-F174-440D-9A38-3A0F0780C163"),
                WeiXinSysTypeName="群发",
                WeiXinAppId=wechat_id,
                WeiXinArticleId=model.Id
            };
            urlService.Add(urlModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        [ValidateInput(false)]
        public JsonResult Edit(WeiXinArticleModel model) 
        {
            DoHandle doHandle = new DoHandle();
            modelSer.Edit(model, out doHandle);
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
            string filePath = Server.MapPath("/images/fileupload/" + fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            postedFile.SaveAs(filePath);
            return Json("../images/fileupload/" + fileName, "text/html");
        }
        #endregion


    }
}
