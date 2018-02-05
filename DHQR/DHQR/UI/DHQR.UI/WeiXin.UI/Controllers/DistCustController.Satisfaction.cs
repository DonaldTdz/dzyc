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
    /// 满意度分析
    /// </summary>
    public partial class DistCustController : BaseController
    {

        #region 分析单个配送员每天满意度

        /// <summary>
        /// 分析单个配送员每天满意度
        /// </summary>
        /// <returns></returns>
          [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult SatisfyByDay()
        {
            RetailerModelService modelService = new RetailerModelService();
            DistDlvmanModelService carService = new DistDlvmanModelService();
            ViewData["RecieveTypeList"] = modelService.GetRecieveTypeList(null);
            ViewData["DlvManList"] = carService.GetDlvManList();
            return View();
        }


          /// <summary>
          /// 获取单个配送员每天满意度信息
          /// </summary>
          /// <returns></returns>
          [IgnoreModule(IgnoreType.IgnoreModule)]
          public JsonResult GetSatisfyByDayData(DistCustByDayQueryParam queryParam)
          {
              DistCustModelService modelService = new DistCustModelService();
              var data = modelService.GetSatisfyByDayData(queryParam);
              return Json(data, JsonRequestBehavior.AllowGet);
          }

        #endregion


        #region  按柱状图展示各个线路满意度指标

          /// <summary>
          /// 按柱状图展示各个线路满意度指标
          /// </summary>
          /// <returns></returns>
          [IgnoreModule(IgnoreType.IgnoreModule)]
          public ActionResult SatisfyOfRete()
          {
              DistCustModelService modelService = new DistCustModelService();
              ViewData["SatisfyName"] = modelService.GetSatisfyList();
              return View();
          }

          /// <summary>
          /// 获取时间段内满意度指标【按柱状图展示各个线路信息】
          /// </summary>
          /// <returns></returns>
          [IgnoreModule(IgnoreType.IgnoreModule)]
          public JsonResult GetSatisfyOfRete(SatisfyQueryParam queryParam)
          {
              DistCustModelService modelService = new DistCustModelService();
              var data = modelService.GetSatisfyOfRete(queryParam);
              return Json(data, JsonRequestBehavior.AllowGet);
          }



        #endregion


        #region 饼状图分析不满意原因

        /// <summary>
        /// 不满意原因分析
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]  
        public ActionResult NotSatisReason()
        {
            DistDlvmanModelService carService = new DistDlvmanModelService();
            ViewData["DlvManList"] = carService.GetDlvManList();
            return View();
        }

        /// <summary>
        /// 获取时间段内满意度指标【按柱状图展示各个线路信息】
        /// </summary> 
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetNotSatisReasons(SatisfyQueryParam queryParam)
        {
            DistCustModelService modelService = new DistCustModelService();
            var data = modelService.GetNotSatisReasons(queryParam);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region 满意度表格数据

        /// <summary>
        /// 满意度表格数据
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult TableData()
        {
            DistDlvmanModelService carService = new DistDlvmanModelService();
            ViewData["DlvManList"] = carService.GetDlvManList();
            return View();
        }


        /// <summary>
        /// 查询满意度数据
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult QuerySatisfaction(SatisfyQueryParam queryParam)
        {
            DistCustModelService modelService = new DistCustModelService();
            var data = modelService.QuerySatisfaction(queryParam);
            return JsonForGrid(data);
        }



        #endregion
    }
}
