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

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 车辆运行控制层
    /// </summary>
    public class DistCarRunController : BaseController
    {


        private readonly DistCarRunModelService modelService;

        public DistCarRunController()
        {
            modelService = new DistCarRunModelService();
        }

     

        #region 【折线图】按费用走势分析

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult AnalysisByTrend()
        {
            LdmDistCarModelService lineService = new LdmDistCarModelService();
            ViewData["DeliveryLineList"] = lineService.GetLineList(true);
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetAnalysisByTrend(DistCarRunQueryParam queryParam)
        {
            var result = modelService.GetAnalysisByTrend(queryParam);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 【柱状图】各车辆费用比较

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult CompareCarFee()
        {
            LdmDistCarModelService lineService = new LdmDistCarModelService();
          //  ViewData["DeliveryLineList"] = lineService.GetLineList(false);
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetCompareCarFeeData(DistCarRunQueryParam queryParam)
        {
            var result = modelService.GetCompareCarFeeData(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 【柱状图】选择年月   展示各个车辆的同比增长率柱状图

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult YearRateOfCarMonth()
        {
            ViewData["YearList"] = modelService.GetYearList();
            ViewData["MonthList"] = modelService.GetMonthList(); 
            return View();
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetYearRateOfCarMonthData(DistCarRunRateQueryParam queryParam)
        {
          //  queryParam.CAR_ID = "GYd9c8961218181801121866bcc9002e";
            var data = modelService.GetYearRateOfCarMonthData(queryParam);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region 【柱状图】选择年、车辆 展示这个车辆在这年各月的同比增长率柱状图

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult YearRateOfCarYear()
        {
            LdmDistCarModelService lineService = new LdmDistCarModelService();
            ViewData["DeliveryLineList"] = lineService.GetLineList(false);
            ViewData["YearList"] = modelService.GetYearList();
            ViewData["MonthList"] = modelService.GetMonthList(); 
            return View();
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetYearRateOfCarYearData(DistCarRunRateQueryParam queryParam)
        {
            //queryParam.CAR_ID = "GYd9c8961218181801121866bcc9002e";
            var data = modelService.GetYearRateOfCarYearData(queryParam);
            return Json(data, JsonRequestBehavior.AllowGet);

        }



        #endregion

        #region 【柱状图】选择年 、车辆 展示这个车辆在这年个月的环比增长率柱状图

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult LinkRateOfCar()
        {
            LdmDistCarModelService lineService = new LdmDistCarModelService();
            ViewData["DeliveryLineList"] = lineService.GetLineList(false);
            ViewData["YearList"] = modelService.GetYearList();
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetLinkRateOfCarData(DistCarRunRateQueryParam queryParam)
        {
           // queryParam.CAR_ID = "GYd9c8961218181801121866bcc9002e";
            var data = modelService.GetLinkRateOfCarData(queryParam);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region 车辆运行数据

        // GET: /DistCarRun/
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
        public JsonResult QueryDistCarRun(DistCarRunQueryParam queryParam)
        {
            var result = modelService.QueryDistCarRun(queryParam);
            return JsonForGrid(result);
        }

        #endregion
    }
}
