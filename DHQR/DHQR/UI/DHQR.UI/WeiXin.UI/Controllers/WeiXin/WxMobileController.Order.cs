using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.DataAccess.Entities;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 订单操作
    /// </summary>
    public partial class WxMobileController : MobileBaseController
    {
        #region 配送单信息

        /// <summary>
        /// 配送单查询页面
        /// </summary>
        /// <param name="WxUserName">微信openId</param>
        /// <param name="key">微信key</param>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public ActionResult DistIndex(string WxUserName, string key)
        {
            ViewData["WxUserName"] = WxUserName;
            ViewData["WeiXinAppName"] = WeiXinAppName;
            var wxUser = new WeiXinUserModelService().GetByWxUserName(WxUserName, WeiXinAppId.Value);
            ViewData["WxUserRealName"] = wxUser.Name;
            ViewData["key"] = key;
            return View();
        }

        /// <summary>
        /// 查询配送单
        /// </summary>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public JsonResult QueryDist(LdmDistQueryParam param)
        {
            LdmDistModelService service = new LdmDistModelService();
            var datas = service.QueryLdmDist(param);
            var result = JsonForGrid(datas);
            return result;
        }


        #endregion



        #region 订单信息

        /// <summary>
        /// 订单查询页面
        /// </summary>
        /// <param name="WxUserName">微信openId</param>
        /// <param name="key">微信key</param>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public ActionResult OrderIndex(string WxUserName,string key,string DIST_NUM,string DistDate)
        {
            ViewData["WxUserName"] = WxUserName;
            ViewData["WeiXinAppName"] = WeiXinAppName;
            var wxUser = new WeiXinUserModelService().GetByWxUserName(WxUserName, WeiXinAppId.Value);
            ViewData["WxUserRealName"] = wxUser.Name;
            ViewData["DIST_NUM"] = DIST_NUM;
            ViewData["DistDate"] = DistDate;
            return View();
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public JsonResult QueryOrder(LdmDistLineQueryParam param)
        {
            LdmDistLineModelService service = new LdmDistLineModelService();
            var datas = service.QueryOrders(param);
            var result = JsonForGrid(datas);
            return result;
        }

        #endregion

        #region 订单明细信息

        /// <summary>
        /// 订单明细
        /// </summary>
        /// <param name="UniCode"></param>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        [HttpGet]
        public ActionResult OrderDetail(string CO_NUM,string WUserName)
        {
            ViewData["CO_NUM"] = CO_NUM;
            ViewData["WeiXinAppName"] = WeiXinAppName;
            ViewData["WxUserRealName"] = WUserName;
            return View();
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public JsonResult QueryOrderDetails(string CO_NUM)
        {
            LdmDisItemModelService itemService = new LdmDisItemModelService();
            var datas = itemService.GetByCoNum(CO_NUM);
            var result = JsonForGrid(datas);
            return result;
        }


        #endregion

        #region 物流信息

        /// <summary>
        /// 物流信息
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        public ActionResult LogisticInfo(string WxUserName, string key)
        {
            ViewData["WeiXinAppName"] = WeiXinAppName;
            return View();
        }

        /// <summary>
        /// 物流信息明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [MobileIgnore(MobileIgnoreType.IgnoreModule)]
        [HttpGet]
        public ActionResult LogisticDetail(string UniCode)
        {
            WeiXinLogisticModelService modelService = new WeiXinLogisticModelService();
            ViewData["WeiXinAppName"] = WeiXinAppName;
            var result = modelService.GetByUniCode(UniCode);
            return View(result);
        }

        #endregion

    }
}
