using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class WeiXinSysTypeController : WeiXinBaseController
    {
        //
        // GET: /WeiXInSysType/

        public ActionResult Index()
        {
            return View();
        }

        

        //新增自定义URL
        public JsonResult Create(WeiXinSysTypeModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            model.WeiXinAppId = wechat_id;
            WeiXinSysTypeModelService modelService = new WeiXinSysTypeModelService();
            modelService.Create(model, out doHandle);
            return Json(doHandle);

        }

        //编辑自定义URL
        public JsonResult Edit(WeiXinSysTypeModel model)
        {
            DoHandle doHandle;
            model.WeiXinAppId = wechat_id;
            WeiXinSysTypeModelService modelService = new WeiXinSysTypeModelService();
            modelService.Update(model, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinSysTypeModelService modelService = new WeiXinSysTypeModelService();
            modelService.Delete("WeiXInSysTypes", id, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinSysTypeModelService modelService = new WeiXinSysTypeModelService();
            PagedResults<WeiXinSysTypeModel> result = modelService.GetPageData(param);

            return JsonForGrid(result);
        }

        /// <summary>
        /// 根据APPID获取模块类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetByAppId()
        {
            WeiXinSysTypeModelService typeService = new WeiXinSysTypeModelService();
            var result = typeService.GetByAppId(wechat_id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

    }
}
