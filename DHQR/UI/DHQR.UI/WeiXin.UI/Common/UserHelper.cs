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

namespace DHQR.UI.DHQRCommon
{
    /// <summary>
    /// 用户辅助类
    /// </summary>
    public static class UserHelper
    {

        /// <summary>
        /// 获取登录名
        /// </summary>
        /// <returns></returns>
        public static string GetLogonName()
        {
            var cooKies = CooKies.GetCookies("token");
            return EncryptTool.Decrypt(cooKies);
        }
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns></returns>
        public static string GetUserKey()
        {
            var cooKies = CooKies.GetCookies("token_Key");
            return EncryptTool.Decrypt(cooKies);
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <returns></returns>
        public static Guid GetUserId()
        {
            var cooKies = CooKies.GetCookies("user_id");
            var idstr= EncryptTool.Decrypt(cooKies);
            return Guid.Parse(idstr);
        }

        /// <summary>
        /// 获取当前用户所有信息
        /// </summary>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            UserLogic logic = new UserLogic();
            var logonName = GetLogonName();
            User user = logic.GetByName(logonName);
            return user;
        }

    

        /// <summary>
        /// 用户是否有Controller和Action对应功能的权限
        /// </summary>
        /// <param name="logonName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(string logonName, string controllerName,
                                               string actionName)
        {
            var logic = new UserLogic();
            DoHandle dohandle;
            var result = logic.HasModuleAuthority(logonName, controllerName, actionName, out dohandle);
            return result;
            //return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(string controllerName, string actionName)
        {
            var logonName = GetLogonName();
            var logic = new UserLogic();
            DoHandle dohandle;
            var result = logic.HasModuleAuthority(logonName, controllerName, actionName, out dohandle);
            return result;
            //return true;
        }

        /// <summary>
        /// 验证用户功能权限
        /// </summary>
        /// <remarks>
        /// <para>先对用户的登录状态进行验证，如果未登录则重定向到系统配置中配置的登录页面，并且终止当前请求Action的执行。</para>
        /// <para>如果已登录，则继续进行功能项权限验证，如果用户没有所请求Action的权限则重定向到权限验证失败页面，并且终止当前请求Action的执行。</para>
        /// <para>如果权限验证通过则继续执行所请求的Action</para> 
        /// </remarks>
        public static bool ValidateUserFeatureAuthority(ActionExecutingContext actionExecutingContext,
                                                        string userName)
        {
            IgnoreModuleAttribute ignoreAuthorityAttribute = GetIgnoreAuthorityAttribute(actionExecutingContext);
            //配置了忽略登录检查特性
            if (ignoreAuthorityAttribute != null && ignoreAuthorityAttribute.IgnoreType == IgnoreType.IgnoreLogon)
            {
                return true;
            }
            //验证是否登录
            if (string.IsNullOrEmpty(userName))
            {
                RedirectToLogonPage(actionExecutingContext);
                return false;
            }
            string logonName = userName;
            if (IsSuperAdmin(logonName))
            {
                return true;
            }
            //检查权限
            string controllerName = actionExecutingContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
            string actionName = actionExecutingContext.ActionDescriptor.ActionName;
            if (ignoreAuthorityAttribute != null)
            {
                //配置了忽略功能项检查
                if (ignoreAuthorityAttribute.IgnoreType == IgnoreType.IgnoreModule)
                    return true;
                //配置了与某个功能项相同的权限检查
                if (ignoreAuthorityAttribute.IgnoreType == IgnoreType.SameAs)
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
            bool passed = HasModuleAuthority(logonName, controllerName, actionName);
            if (!passed)
            {
                RedirectToAuthorityFailPage(actionExecutingContext);
            }
            return passed;
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <param name="logonName"></param>
        /// <returns></returns>
        public static bool IsSuperAdmin(string logonName)
        {
            var result = new UserLogic().IsSuperAdmin(logonName);
            return result;
        }

        /// <summary>
        /// 指定的Action是否需要权限检查
        /// </summary>
        /// <param name="actionExecutingContext"></param>
        /// <returns></returns>
        private static IgnoreModuleAttribute GetIgnoreAuthorityAttribute(ActionExecutingContext actionExecutingContext)
        {
            //配置了忽略权限检查
            object[] attributes =
                actionExecutingContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreModuleAttribute), false);
            return attributes.Length == 0 ? null : (IgnoreModuleAttribute)attributes[0];
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
        /// 重定向到权限登录页面
        /// </summary>
        /// <param name="actionExecutingContext"></param>
        private static void RedirectToLogonPage(ActionExecutingContext actionExecutingContext)
        {

            string url = string.Format("{0}?returnUrl={1}", "/Account/Login",
                                       HttpUtility.UrlEncode(
                                           actionExecutingContext.RequestContext.HttpContext.Request.Url.ToString()));
            actionExecutingContext.Result = new RedirectResult(url);
        }



    }
}