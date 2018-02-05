using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 微博
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult WeiBoReport()
        {
            return View();
        }

        /// <summary>
        /// 房价（均价百分比）
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult HousePriceReport()
        {
            return View();
        }

        /// <summary>
        /// 房价（按时间走势）
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult HousePrice()
        {
            return View();
        } 

        /// <summary>
        /// 房价（按楼盘分析）
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult HousePriceByArea()
        {
            return View();
        }

        /// <summary>
        /// 微博接口调试
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult WebData()
        {
            return View();
        }


    }
}
