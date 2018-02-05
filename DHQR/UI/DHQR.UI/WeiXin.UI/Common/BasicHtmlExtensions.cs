using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.UI.CooKies;
using Common.UI.EncryptTool;

namespace DHQR.UI.DHQRCommon
{
    /// <summary>
    /// 基础的Html扩展方法
    /// </summary>
    public static class BasicHtmlExtensions
    {
        private const string defaultLanguageCode = "ZH-CN";
        #region 当前用户

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
        /// 获取当前用户
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static User CurrentUser(this HtmlHelper htmlHelper)
        {
            UserLogic logic = new UserLogic();
            var logonName = GetLogonName();
            User user = logic.GetByName(logonName);
            return user;
        }

        #endregion
        #region 权限验证

        /// <summary>
        /// 用户是否有Controller和Action对应功能的权限
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(this HtmlHelper helper, string controllerName,
                                               string actionName)
        {
            return UserHelper.HasModuleAuthority(helper.CurrentUser().Name, controllerName, actionName);
        }


        /// <summary>
        /// 用户是否有当前Controller和Action对应功能的权限
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasModuleAuthority(this HtmlHelper helper, string actionName)
        {
            var currentUser = helper.CurrentUser();
            if (currentUser == null)
            {
                return false;
            }
            return UserHelper.HasModuleAuthority(currentUser.Name, helper.ViewContext.Controller.GetType().FullName, actionName);
        }

        /// <summary>
        /// 当条件为真时才输出
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString Condition(this HtmlHelper helper, bool canOutput, string value)
        {
            if (!canOutput)
            {
                return MvcHtmlString.Empty;
            }
            return MvcHtmlString.Create(value);
        }

        #endregion
    }
}