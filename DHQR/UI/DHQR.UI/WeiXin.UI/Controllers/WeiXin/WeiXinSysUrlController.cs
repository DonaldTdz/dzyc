using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 系统URL
    /// </summary>
    public class WeiXinSysUrlController : WeiXinBaseController
    {
        //
        // GET: /WeiXinSysUrl/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult GetByTypeId(Guid TypeId)
        {
            WeiXinSysUrlModelService service = new WeiXinSysUrlModelService();
            var result = service.GetByTypeId(TypeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditOrAdd(Guid? Id)
        {
            return null;
        }

        //新增自定义URL
        public JsonResult Create(WeiXinSysUrlModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            model.WeiXinAppId = wechat_id;
            WeiXinSysUrlModelService modelService = new WeiXinSysUrlModelService();
            modelService.Create(model, out doHandle);
            return Json(doHandle);

        }

        //编辑自定义URL
        public JsonResult Edit(WeiXinSysUrlModel model)
        {
            DoHandle doHandle;
            model.WeiXinAppId = wechat_id;
            WeiXinSysUrlModelService modelService = new WeiXinSysUrlModelService();
            modelService.Update(model, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinCustomUrlModelService modelService = new WeiXinCustomUrlModelService();
            modelService.Delete("WeiXinSysUrls", id, out doHandle);
            return JsonForDoHandle(doHandle);


        }

    }
}
