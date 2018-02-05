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
    /// 配送点预警
    /// </summary>
    public class DeliveryWarningController : BaseController
    {


       private readonly  DeliveryWarningModelService modelService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryWarningController()
        {
            modelService = new DeliveryWarningModelService();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            LdmDistCarModelService lineService = new LdmDistCarModelService();
            ViewData["DeliveryLineList"] = lineService.GetLineList(false);
            return View();
        }


        /// <summary>
        /// 获取配送点预警信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetWarningDatas( )
        {
            DeliveryWarningQueryParam queryParam = new DeliveryWarningQueryParam { DIST_NUM = "GYPS000000120587" };
            var result = modelService.GetWarningDatas(queryParam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
