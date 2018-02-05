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
    /// 配送员位置信息
    /// </summary>
    public class GisLastLocrecordController : BaseController
    {

        /// <summary>
        /// 配送员实时位置页面
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取实时定位信息
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetLatestGisInfos()
        {
            GisLastLocrecordModelService modelService=new GisLastLocrecordModelService();
            var result = modelService.GetLatestGisInfos();
            return Json(result,JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 配送员实际线路
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult DlvmanLine()
        {
            DistDlvmanModelService manService = new DistDlvmanModelService();
            ViewData["DlvManList"] = manService.GetDlvManList();
            return View();
        }

        /// <summary>
        /// 获取配送员轨迹坐标
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetGisInfosOfDlvman(GisLastLocrecordQueryParam queryParam)
        {
            GisLastLocrecordModelService modelService=new GisLastLocrecordModelService();
           
            var result = modelService.GetGisInfosOfDlvman(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
