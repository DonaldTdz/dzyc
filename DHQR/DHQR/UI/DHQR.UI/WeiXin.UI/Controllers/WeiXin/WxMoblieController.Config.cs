using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 配置、账号绑定
    /// </summary>
    public partial class WxMobileController : MobileBaseController
    {

        /// <summary>
        /// 绑定账号
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult Bind(string WxUserName, string key)
        {
            //检查是否已经绑定
            WeiXinAppModelService modelService = new WeiXinAppModelService();
            var weiXinAppId = modelService.GetByWeiXinKey(key).Id;
            bool hasBind = new WeiXinUserModelService().CheckHasBind(WxUserName, weiXinAppId);
            ViewData["HasBind"] = hasBind;
            WeiXinUserModel model = new WeiXinUserModel();
            if (hasBind)
            {
                model = new WeiXinUserModelService().GetByWxUserName(WxUserName, weiXinAppId);
                model.key = key;
            }
            else
            {
                model.WxUserName = WxUserName;
                model.key = key;
            }
            return View(model);
        }


        /// <summary>
        /// 绑定账号（第二版）
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult BindNew(string WxUserName, string key)
        {
            //检查是否已经绑定
            WeiXinAppModelService modelService = new WeiXinAppModelService();
            var weiXinAppId = modelService.GetByWeiXinKey(key).Id;
            bool hasBind = new WeiXinUserModelService().CheckHasBind(WxUserName, weiXinAppId);
            ViewData["HasBind"] = hasBind;
            WeiXinUserModel model = new WeiXinUserModel();
            if (hasBind)
            {
                model = new WeiXinUserModelService().GetByWxUserName(WxUserName, weiXinAppId);
                model.key = key;
            }
            else
            {
                model.WxUserName = WxUserName;
                model.key = key;
            }
            return View(model);
        }



        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        [HttpPost]
        public JsonResult BindUser(WeiXinUserModel model)
        {
            DoHandle dohandle;
            WeiXinUserModelService service = new WeiXinUserModelService();
            service.BindUser(model, out dohandle);
            return JsonForDoHandle(dohandle);//RedirectToAction("BindUser");
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        [HttpPost]
        public JsonResult RemoveBind(WeiXinUserModel model)
        {
            DoHandle dohandle;
            WeiXinUserModelService service = new WeiXinUserModelService();
            service.RemoveBind(model, out dohandle);
            return JsonForDoHandle(dohandle);//RedirectToAction("BindUser");
        }



        /// <summary>
        /// 程序正在开发中
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult Working()
        {
            return View();
        }


        /// <summary>
        /// 账号未绑定
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult NotBind()
        {
            return View();
        }


        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult Test()
        {
            return View();
        }


        /// <summary>
        /// 绑定账号
        /// </summary>
        /// <returns></returns>
        [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult BindTest(string WxUserName, string key)
        {
            //检查是否已经绑定
            WeiXinAppModelService modelService = new WeiXinAppModelService();
            var weiXinAppId = modelService.GetByWeiXinKey(key).Id;
            bool hasBind = new WeiXinUserModelService().CheckHasBind(WxUserName, weiXinAppId);
            ViewData["HasBind"] = hasBind;
            WeiXinUserModel model = new WeiXinUserModel();
            if (hasBind)
            {
                model = new WeiXinUserModelService().GetByWxUserName(WxUserName, weiXinAppId);
            }
            else
            {
                model.WxUserName = WxUserName;
                model.key = key;
            }
            return View(model);
        }



    }
}
