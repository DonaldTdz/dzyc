using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using Common.UI.CooKies;
using Common.UI.EncryptTool;
using DHQR.UI.Models;

namespace DHQR.UI.DHQRCommon
{
    /// <summary>
    /// 手机web辅助类
    /// </summary>
    public class MobileWebHelper
    {


        /// <summary>
        /// 用户是否有Controller和Action对应功能的权限
        /// </summary>
        /// <param name="logonName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(string WxUserName, Guid WeiXinappId, string controllerName,
                                               string actionName)
        {
            var logic = new WeiXinUserLogic();
            DoHandle dohandle;
            var result = logic.HasModuleAuthority(WxUserName, WeiXinappId, controllerName, actionName, out dohandle);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(string controllerName, string actionName)
        {
            var WxUserName = GetWxUserName();
            var WeiXinappId = GetWeiXinId();
            var logic = new WeiXinUserLogic();
            DoHandle dohandle;
            var result = logic.HasModuleAuthority(WxUserName, WeiXinappId.Value, controllerName, actionName, out dohandle);
            return result;
        }

        /// <summary>
        /// 验证手机微信用户权限
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static bool ValidateMobileAuthority(ActionExecutingContext actionExecutingContext, string WxUserName, Guid? WeiXinappId)
        {
            MobileIgnoreAttribute ignoreAuthorityAttribute = GetMobileIgnoreAuthorityAttribute(actionExecutingContext);
            //配置了忽略登录检查特性
            if (ignoreAuthorityAttribute != null && ignoreAuthorityAttribute.IgnoreType == MobileIgnoreType.IgnoreCheck)
            {
                return true;
            }
            if (string.IsNullOrEmpty(WxUserName) || !WeiXinappId.HasValue)
            {
                RedirectToAuthorityFailPage(actionExecutingContext);
                return false;
            }
            //检查是否绑定账号
            var hasBind = new WeiXinUserModelService().CheckHasBind(WxUserName, WeiXinappId.Value);
            if (!hasBind)
            {
                var wxKey = new WeiXinAppModelService().GetByKey(WeiXinappId.Value).WeiXinKey;
                RedirectToLogonPage(actionExecutingContext, WxUserName, wxKey);
                return false;
            }

            //检查功能权限
            string controllerName = actionExecutingContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
            string actionName = actionExecutingContext.ActionDescriptor.ActionName;
            if (ignoreAuthorityAttribute != null)
            {
                if (ignoreAuthorityAttribute.IgnoreType == MobileIgnoreType.IgnoreModule)
                {
                    return true;
                }
                if (ignoreAuthorityAttribute.IgnoreType == MobileIgnoreType.SameAs)
                {
                    if (string.IsNullOrEmpty(ignoreAuthorityAttribute.SameActionName))
                    {
                        throw new InvalidOperationException(string.Format("Controller：{0}上的Action：{1}配置了IgnoreType.SameAs特性，但没有配置对应的SameAsActionName", controllerName, actionName));
                    }
                    actionName = ignoreAuthorityAttribute.SameActionName;
                    if (!string.IsNullOrEmpty(ignoreAuthorityAttribute.SameControllerName))
                    {
                        controllerName = ignoreAuthorityAttribute.SameControllerName;
                    }

                }
            }
            //验证功能项权限
            bool passed = HasModuleAuthority(WxUserName, WeiXinappId.Value, controllerName, actionName);
            if (!passed)
            {
                RedirectToAuthorityFailPage(actionExecutingContext);
            }
            return passed;




        }

        /// <summary>
        /// 指定的Action是否需要权限检查
        /// </summary>
        /// <param name="actionExecutingContext"></param>
        /// <returns></returns>
        private static MobileIgnoreAttribute GetMobileIgnoreAuthorityAttribute(ActionExecutingContext actionExecutingContext)
        {
            //配置了忽略权限检查
            object[] attributes =
                actionExecutingContext.ActionDescriptor.GetCustomAttributes(typeof(MobileIgnoreAttribute), false);
            return attributes.Length == 0 ? null : (MobileIgnoreAttribute)attributes[0];
        }

        /// <summary>
        /// 重定向到权限验证失败页面
        /// </summary>
        private static void RedirectToAuthorityFailPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new EmptyResult();
            const string message = "您没有配置该页面权限";
            filterContext.HttpContext.Response.Write(message);
        }



        /// <summary>
        /// 重定向到绑定页面
        /// </summary>
        /// <param name="actionExecutingContext"></param>
        private static void RedirectToLogonPage(ActionExecutingContext actionExecutingContext, string WxUserName, string key)
        {

            string url = string.Format("{0}?WxUserName={1}&key={2}&returnUrl={3}", "/WxMobile/NotBind", WxUserName, key,
                                       HttpUtility.UrlEncode(
                                           actionExecutingContext.RequestContext.HttpContext.Request.Url.ToString()));
            actionExecutingContext.Result = new RedirectResult(url);
        }

        /// <summary>
        /// 获取微信用户名
        /// </summary>
        /// <returns></returns>
        public static string GetWxUserName()
        {
            var cooKies = CooKies.GetCookies("WxUserName");
            return EncryptTool.Decrypt(cooKies);
        }
        /// <summary>
        /// 获取微信Id
        /// </summary>
        /// <returns></returns>
        public static Guid? GetWeiXinId()
        {
            var cooKies = CooKies.GetCookies("MobileWxId");
            var idstr = EncryptTool.Decrypt(cooKies);
            if (string.IsNullOrEmpty(idstr))
            {
                return null;
            }
            else
            {
                var result = Guid.Parse(idstr);
                return result;
            }
        }

        /// <summary>
        /// 获取微信名称
        /// </summary>
        /// <returns></returns>
        public static string GetWeiXinName()
        {
            var cooKies = CooKies.GetCookies("MobileWxName");
            var name = EncryptTool.Decrypt(cooKies);
            return name;

        }


    }
}