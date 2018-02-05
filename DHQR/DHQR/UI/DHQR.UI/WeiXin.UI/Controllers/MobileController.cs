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
    public class MobileController : BaseController
    {
        //
        // GET: /Mobile/

        /// <summary>
        /// 导航
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取实时定位信息
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetLatestGisInfos()
        {
            GisLastLocrecordModelService modelService = new GisLastLocrecordModelService();
            var result = modelService.GetLatestGisInfos();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取配送线路信息
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetDeliveryLine()
        {
            LdmDistLineModelService lineService = new LdmDistLineModelService();
            LdmDistLineQueryParam queryParam = new LdmDistLineQueryParam { DIST_NUM = "GYPS000000120699" };
            var result = lineService.QueryDeliveryLine(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}
