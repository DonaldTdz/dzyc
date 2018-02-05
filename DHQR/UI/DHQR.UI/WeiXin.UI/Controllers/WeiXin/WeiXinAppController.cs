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
    public class WeiXinAppController : WeiXinBaseController
    {
        //
        // GET: /WeiXinApp/

        private readonly WeiXinAppModelService weiXinAppService;


        public WeiXinAppController()
        {
            weiXinAppService = new WeiXinAppModelService();
        }

        public ActionResult Index()
        {

            var currentApp = weiXinAppService.GetByKey(wechat_id);
            return View(currentApp);
        }


        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 微信接口配置
        /// </summary>
        /// <returns></returns>
        public ActionResult InterfaceCfg()
        {

            var currentApp = weiXinAppService.GetByKey(wechat_id);
            if (currentApp != null)
            {
                currentApp.Url = "http://" + HttpContext.Request.Url.Authority + currentApp.Url;
            }

            return View(currentApp);
        }


        //保存
        [HttpPost]
        public JsonResult Save(WeiXinAppModel model)
        {
            DoHandle dohandle=new DoHandle();
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                model.WeiXinKey = Guid.NewGuid().ToString();
                model.UserId = GetCurrentUser().Id;
                model.CreateTime = DateTime.Now;
                model.Url = "/WeiXinApi/Index?key="+model.WeiXinKey;   
                weiXinAppService.Create(model, out dohandle);
            }
            else
            {
                model.Url = "/WeiXinApi/Index?key=" + model.WeiXinKey;   
                weiXinAppService.Update(model, out dohandle);
            }
            return JsonForDoHandle(dohandle);
        }
    }
}
