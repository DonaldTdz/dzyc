using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;
using DHQR.DataAccess.Entities;
using DHQR.BasicLib;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 配送任务
    /// </summary>
    public class LdmDistController : BaseController
    {
        private LdmDistModelService modelService;
        private LdmDistLineModelService lineService;
        private LdmDisItemModelService itemService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LdmDistController()
        {
            modelService = new LdmDistModelService();
            lineService = new LdmDistLineModelService();
            itemService = new LdmDisItemModelService();
        }

        #region 配送单查询

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult QueryLdmDist(LdmDistQueryParam queryParam)
        {
            var result = modelService.QueryLdmDistPc(queryParam);
            return JsonForGrid(result);
        }




        /// <summary>
        /// 订单页面
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult OrderInfo(string DIST_NUM)
        {
            ViewData["DIST_NUM"] = DIST_NUM;
            return View();
        }


        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult QueryLdmdistLine(LdmDistLineQueryParam queryParam)
        {
            var result = lineService.QueryLdmdistLine(queryParam);
            return JsonForGrid(result);
        }


        /// <summary>
        /// 手工到货确认
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult HandleConfirm(string CO_NUM, string DIST_NUM)
        {
            DoHandle dohandle;
            modelService.HandleConfirm(CO_NUM, DIST_NUM, out dohandle);
            return JsonForDoHandle(dohandle);
        }





        #endregion


        #region 配送线路查询


        /// <summary>
        /// 配送线路页面
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult DeliveryLine()
        {
            DistRutModelService rutService = new DistRutModelService();
            ViewData["DeliveryLineList"] = rutService.GetRutList(false);
            return View();
        }

        /// <summary>
        /// 获取配送线路信息
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetDeliveryLine(LdmDistLineQueryParam queryParam)
        {
            var result = lineService.QueryDeliveryLine(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region 配送任务完成率


        /// <summary>
        /// 配送任务完成率页面
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult DeliveryFinish()
        {
           
            return View();
        }

        /// <summary>
        /// 查询配送任务完成率
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetLdmDistFinishRate(LdmFinishQueryParam queryParam)
        {
            var result = modelService.GetLdmDistFinishRate(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}
