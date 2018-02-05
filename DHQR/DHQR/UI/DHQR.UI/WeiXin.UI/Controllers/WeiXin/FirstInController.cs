using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class FirstInController : WeiXinBaseController
    {

        public ActionResult Index()
        {
            WeiXinPicMsgMatserModelService picMsgModelService = new WeiXinPicMsgMatserModelService();
            ViewData["WeiXinSourceSelect"] = picMsgModelService.creatSelectList();
            WeiXinTriggerInfoModelService triggerInfoService = new WeiXinTriggerInfoModelService();
            ViewData["TriggerInfo"] = triggerInfoService.creatSelectList();

            WeiXinFirstInModelService firstInModelService = new WeiXinFirstInModelService();
            WeiXinFirstInModel model = firstInModelService.GetByWeiXinAppId(wechat_id);
            return View(model);
        }

        //新增
        public JsonResult Create(WeiXinFirstInModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            model.ContenInfo = model.ContenInfo == null ? "" : model.ContenInfo;
            WeiXinFirstInModelService modelService = new WeiXinFirstInModelService();
            model.WeiXinAppId = wechat_id;
            modelService.Create(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //编辑
        public JsonResult Edit(WeiXinFirstInModel model)
        {
            DoHandle doHandle;
            WeiXinFirstInModelService modelService = new WeiXinFirstInModelService();

            modelService.EditFirstInMsg(model, out doHandle);
            return JsonForDoHandle(doHandle);

        } 

    }
}
