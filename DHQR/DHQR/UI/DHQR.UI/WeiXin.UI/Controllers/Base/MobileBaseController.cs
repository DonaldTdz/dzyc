using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using Common.UI.CooKies;
using Common.UI.EncryptTool;
using DHQR.DataAccess.Entities;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{

    /// <summary>
    /// 手机web基础控制层
    /// </summary>
    public abstract class MobileBaseController : Controller
    {
        /// <summary>
        /// 微信用户名
        /// </summary>
        public string WxUserName { set; get; }

        /// <summary>
        /// 微信Key
        /// </summary>
        public Guid? WeiXinAppId { set; get; }


        /// <summary>
        /// 微信公司名
        /// </summary>
        public string WeiXinAppName { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public MobileBaseController()
        {
        }

        /// <summary>
        /// 设置手机Cookie
        /// </summary>
        private void SetMobileCookies()
        {
            var currentQueryUser = Request.QueryString["WxUserName"];//查询参数 用户名
            var currentQueryKey = Request.QueryString["key"];//查询参数 微信Key
            if (!string.IsNullOrEmpty(currentQueryUser))//如果不为空 则检查是否与Cookies一致
            {
                var cookieUserName = MobileWebHelper.GetWxUserName();
                if (currentQueryUser != cookieUserName)//如果不一致，则重新设置Cookie
                {
                    CooKies.SetCookies("WxUserName", EncryptTool.Encrypt(currentQueryUser));//登录名
                    WxUserName = currentQueryUser;
                    //获取WeiXinAppId
                    var wxApp = new WeiXinAppModelService().GetByWeiXinKey(currentQueryKey);
                    if (wxApp != null)
                    {
                        CooKies.SetCookies("MobileWxId", EncryptTool.Encrypt(wxApp.Id.ToString()));
                        CooKies.SetCookies("MobileWxName", EncryptTool.Encrypt(wxApp.Name));
                        WeiXinAppId = wxApp.Id;
                        WeiXinAppName = wxApp.Name;
                    }
                }
                WxUserName = currentQueryUser.Trim();
                WeiXinAppId = MobileWebHelper.GetWeiXinId();
                WeiXinAppName = new WeiXinAppModelService().GetAll().First().Name;//暂时写死
            }
            else
            {
                WxUserName = MobileWebHelper.GetWxUserName();
                WeiXinAppId = MobileWebHelper.GetWeiXinId();
                WeiXinAppName = new WeiXinAppModelService().GetAll().First().Name;//暂时写死
            }
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
            MobileIgnoreAttribute ignoreAuthorityAttribute = GetMobileIgnoreAuthorityAttribute(filterContext);

            SetMobileCookies();
            MobileWebHelper.ValidateMobileAuthority(filterContext, WxUserName, WeiXinAppId);
            base.OnActionExecuting(filterContext);
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
        /// 捕获异常
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ExceptionLogModelService.MobileAddLog(filterContext.Exception);
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



    }
}
