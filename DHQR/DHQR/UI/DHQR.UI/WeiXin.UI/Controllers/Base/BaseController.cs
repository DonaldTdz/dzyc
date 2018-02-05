using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DHQR.BusinessLogic.Implement;
using Common.Base;
using Common.UI.CooKies;
using Common.UI.EncryptTool;
using DHQR.UI.Models;
using DHQR.DataAccess.Entities;
using DHQR.UI.DHQRCommon;
using System.IO;
using System.Runtime.Serialization.Json;


namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 基本控制层
    /// </summary>
    public abstract class BaseController : Controller, IUserInfo
    {

        public string currentUserName { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController()
        {
            currentUserName = GetLogonName();
        }

        /// <summary>
        /// 指定的Action是否需要权限检查
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private static NoAuthorityAttribute GetAuthorityAttribute(ActionExecutingContext filterContext)
        {
            //配置了忽略权限检查
            object[] attributes =
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthorityAttribute), false);
            return attributes.Length == 0 ? null : (NoAuthorityAttribute)attributes[0];
        }


        /// <summary>
        /// 执行Action前检查
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserHelper.ValidateUserFeatureAuthority(filterContext, currentUserName);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ExceptionLogModelService.DoAddLog(filterContext.Exception);
            base.OnException(filterContext);
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
        /// 重定向到登录页面
        /// </summary>
        /// <param name="actionExecutingContext"></param>
        private static void RedirectToLogonPage(ActionExecutingContext actionExecutingContext)
        {

            string url = string.Format("{0}?returnUrl={1}", "/Account/Login",
                                       HttpUtility.UrlEncode(
                                           actionExecutingContext.RequestContext.HttpContext.Request.Url.ToString()));
            actionExecutingContext.Result = new RedirectResult(url);
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetLogonName()
        {
            return UserHelper.GetLogonName();
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserKey()
        {
            return UserHelper.GetUserKey();
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            return UserHelper.GetCurrentUser();
        }


        /// <summary>
        /// 转换成远程分页表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedResults"></param>
        /// <returns></returns>
        protected JsonResult JsonForGrid<T>(PagedResults<T> pagedResults)
        {
            return Json(pagedResults != null
                            ? new
                            {
                                total = pagedResults.PagerInfo.TotalPageCount,
                                page = pagedResults.PagerInfo.PageIndex,
                                records = pagedResults.PagerInfo.TotalRowCount,
                                rows = pagedResults.Data
                            }
                            : null);
        }

        /// <summary>
        /// 转换成本地分页表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult JsonForGrid<T>(IList<T> data)
        {
            return JsonForGrid(data, false);
        }

        /// <summary>
        /// 转换成表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="loadonce"></param>
        /// <returns></returns>
        protected JsonResult JsonForGrid<T>(IList<T> data, bool loadonce)
        {
            if (loadonce)
                return Json(data);
            var fAnonymousType1 = new
            {
                page = 1,
                rows = data
            };
            return Json(fAnonymousType1);
        }

        /// <summary>
        /// 返回结果格式的状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected JsonResult JsonForDoHandle(DoHandle status)
        {
            if (status == null)
                return null;
            return Json(status);
        }

        /// <summary>
        /// 将树的列表转换为供Trre使用的Json对象。
        /// </summary>
        /// <param name="treeList"></param>
        /// <returns></returns>
        protected JsonResult JsonForTree(IList<JsonTree> treeList)
        {
            InitTreeExpendStatus(treeList);
            return base.Json(treeList);
        }

        /// <summary>
        /// 处理树的展开状态
        /// </summary>
        private static void InitTreeExpendStatus(IList<JsonTree> treeList)
        {
            //有多个树时，不展开,只有一颗树时，展开根节点
            if (treeList != null && treeList.Count == 1)
            {
                foreach (JsonTree jsonTree in treeList)
                {
                    jsonTree.isexpand = true;
                }
            }
        }

        /// <summary>
        /// 获取本地缓存页面
        /// </summary>
        /// <returns></returns>
        public string GetPageCookies()
        {
            var cooKies = CooKies.GetCookies("Page_Key");
            return EncryptTool.Decrypt(cooKies);
        }

        /// <summary>
        /// 设置本地缓存页面
        /// </summary>
        /// <param name="url"></param>
        public void SetPageCookies(string url)
        {
            CooKies.SetCookies("Page_Key", EncryptTool.Encrypt(url));
        }

        //Json 字符串转能Model列表
        public static IList<T> JsonStrToModel<T>(string jsonStr)
        {
            MemoryStream memStream = new MemoryStream();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
            StreamWriter wr = new StreamWriter(memStream);
            wr.Write(jsonStr);
            wr.Flush();
            memStream.Position = 0;
            Object obj = jsonSerializer.ReadObject(memStream);
            List<T> result = (List<T>)obj;
            return result;
        }

    }
}
