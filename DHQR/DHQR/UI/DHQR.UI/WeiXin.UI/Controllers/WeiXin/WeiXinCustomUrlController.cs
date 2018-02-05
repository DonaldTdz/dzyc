using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using Common.Base;
using DHQR.BusinessLogic.Implement;
using System.IO;

namespace DHQR.UI.Controllers
{
    public class WeiXinCustomUrlController : WeiXinBaseController
    {
        //
        // GET: /WeiXinCustomUrl/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult EditOrAdd(Guid? Id)
        {
            return null;
        }

        //新增自定义URL
        public JsonResult Create(WeiXinCustomUrlModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            model.WeiXinAppId = wechat_id;
            WeiXinCustomUrlModelService modelService = new WeiXinCustomUrlModelService();
            modelService.Create(model, out doHandle);
            return Json(doHandle);

        }

        //编辑自定义URL
        public JsonResult Edit(WeiXinCustomUrlModel model)
        {
            DoHandle doHandle;
            model.WeiXinAppId = wechat_id;
            WeiXinCustomUrlModelService modelService = new WeiXinCustomUrlModelService();
            modelService.Update(model, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinCustomUrlModelService modelService = new WeiXinCustomUrlModelService();
            modelService.Delete("WeiXinCustomUrls", id, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinCustomUrlModelService modelService = new WeiXinCustomUrlModelService();
            PagedResults<WeiXinCustomUrlModel> result = modelService.GetPageData(param);

            return JsonForGrid(result);
        }



    }
}
