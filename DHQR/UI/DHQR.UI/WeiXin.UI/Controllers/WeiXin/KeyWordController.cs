using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using Common.Base;

namespace DHQR.UI.Controllers
{
    //关键词回复设置
    public class KeyWordController : WeiXinBaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditOrAdd(Guid? Id)
        {
            WeiXinPicMsgMatserModelService picMsgModelService = new WeiXinPicMsgMatserModelService();
            ViewData["WeiXinSourceSelect"] = picMsgModelService.creatSelectList();
            WeiXinTriggerInfoModelService triggerInfoService = new WeiXinTriggerInfoModelService();
            ViewData["TriggerInfo"] = triggerInfoService.creatSelectList();
            WeiXinKeyWordModel model = null;
            if (Id.HasValue)
            {
                WeiXinKeyWordModelService modelService = new WeiXinKeyWordModelService();
                model = modelService.GetByKey(Id.Value);
            }
            return View(model);
        }

        //新增关键字
        public JsonResult Create(WeiXinKeyWordModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            model.ContenInfo = model.ContenInfo == null ? "" : model.ContenInfo;
            WeiXinKeyWordModelService modelService = new WeiXinKeyWordModelService();
            model.WeiXinAppId = wechat_id;
            modelService.Create(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //编辑关键字
        public JsonResult Edit(WeiXinKeyWordModel model)
        {
            DoHandle doHandle;
            WeiXinKeyWordModelService modelService = new WeiXinKeyWordModelService();
         
            modelService.EditKeyWord(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinKeyWordModelService modelService = new WeiXinKeyWordModelService();
            modelService.Delete("WeiXinKeyWords", id, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinKeyWordModelService modelService = new WeiXinKeyWordModelService();
            PagedResults<WeiXinKeyWordModel> result = modelService.GetPageData(param);
            return JsonForGrid(result);
        }
    }
}
