using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using Common.Base;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Controllers
{
    public class WeiXinMenuController : WeiXinBaseController
    {
        //
        // GET: /WeiXinMenu/

        public ActionResult Index()
        {
            WeiXinPicMsgMatserModelService picMsgModelService = new WeiXinPicMsgMatserModelService();
            ViewData["WeiXinSourceSelect"] = picMsgModelService.creatSelectList();
            WeiXinTriggerInfoModelService triggerInfoService = new WeiXinTriggerInfoModelService();
            ViewData["TriggerInfo"] = triggerInfoService.creatSelectList();


            var service = new WeiXinMenuModelService();
            ViewData["TopTree"] = service.Query(f => f.ParentId == null).ToList()
                .Select(f => new SelectListItem()
                {
                    Value = f.Id.ToString(),
                    Text = f.Name
                });

            List<WeiXinMenuModel> models = service.GetAll().ToList();

            return View(models);
        }
        public ActionResult Edit(Guid id)
        {

            WeiXinPicMsgMatserModelService picMsgModelService = new WeiXinPicMsgMatserModelService();
            ViewData["WeiXinSourceSelect"] = picMsgModelService.creatSelectList();
            WeiXinTriggerInfoModelService triggerInfoService = new WeiXinTriggerInfoModelService();
            ViewData["TriggerInfo"] = triggerInfoService.creatSelectList();

            var service = new WeiXinMenuModelService();
            var data = service.GetByKey(id);
            ViewData["TopTree"] = service.Query(f => f.ParentId == null).ToList()
                .Select(f => new SelectListItem()
                {
                    Value = f.Id.ToString(),
                    Text = f.Name,
                    Selected = f.Id == data.ParentId
                });
            List<WeiXinMenuModel> models = service.GetAll().ToList();
            ViewData["CurMenu"] = data;

            var d = new Dictionary<int, string> { { 0, "文字消息" }, { 1, "图文消息" }, { 2, "连接网址" } };

            ViewData["SelType"] = d.Select(f => new SelectListItem()
            {
                Value = f.Key.ToString(),
                Text = f.Value,
                Selected = f.Key == data.Type
            });


            return View(models);
        }

        public JsonResult DeleteMenuId(Guid id)
        {
            DoHandle doHandle;
            new WeiXinMenuLogic().DeleteMenuId(id, out doHandle);
            return JsonForDoHandle(doHandle);
        }





        public ActionResult GetTreeData()
        {
            var service = new WeiXinMenuModelService();
            var tree = service.GetTreeData();
            return Json(tree);
        }

        public JsonResult AdMenu(WeiXinMenuModel model)
        {
            var service = new WeiXinMenuModelService();
            DoHandle doHandle;
            if (model.Id == Guid.Empty)
            {
                switch (model.Type)
                {
                    case (int)WeiXinMenuType.Word:
                        model.WxType = "click";
                        break;
                    case (int)WeiXinMenuType.Pic:
                        model.WxType = "click";
                        break;
                    case (int)WeiXinMenuType.View:
                        model.WxType = "view";
                        break;
                    default:
                        model.WxType = "click";
                        break;
                }
                model.WeiXinAppId = wechat_id;
                model.Key = Guid.NewGuid().ToString();
                service.Create(model, out doHandle);
            }
            else
            {
                switch (model.Type)
                {
                    case (int)WeiXinMenuType.Word:
                        model.WxType = "click";
                        break;
                    case (int)WeiXinMenuType.Pic:
                        model.WxType = "click";
                        break;
                    case (int)WeiXinMenuType.View:
                        model.WxType = "view";
                        break;
                    default:
                        model.WxType = "click";
                        break;
                }          
                service.Update(model, out  doHandle);
            }
            return JsonForDoHandle(doHandle);
        }

        public JsonResult Del(Guid id)
        {
            var service = new WeiXinMenuModelService();
            DoHandle doHandle;
            service.Del(new WeiXinMenuModel() { Id = id }, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        public JsonResult GetByKey(Guid id)
        {
            var service = new WeiXinMenuModelService();
            var data = service.GetByKey(id);
            return Json(data);
        }

        /// <summary>
        /// 发布菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult PublishMenu()
        {
            ConmmonApi api = new ConmmonApi();
            string msg;
            DoHandle doHandle;
            var erroCode= api.SetMenu(wechat_id,out msg);
            if (erroCode == "0")
            {
                doHandle = new DoHandle { IsSuccessful = true, OperateMsg = "菜单发布成功，24小时后可看到效果，或取消关注再重新关注可即时看到效果！" };
            }
            else
            {
                doHandle = new DoHandle { IsSuccessful=false,OperateMsg=msg};
            }
            return JsonForDoHandle(doHandle);
        }

    }
}
