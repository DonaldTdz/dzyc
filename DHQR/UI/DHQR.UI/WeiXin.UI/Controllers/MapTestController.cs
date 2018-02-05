using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;


namespace DHQR.UI.Controllers
{
    public class MapTestController : BaseController
    {
        //
        // GET: /MapTest/



        /// <summary>
        /// 地图首页
        /// </summary>
        /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Index()
        {
            return View();
        }

         /// <summary>
         /// 地图绘点
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public ActionResult AddPoint()
         {
             return View();
         }

         /// <summary>
         /// 轨迹回放
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public ActionResult TrailRecord()
         {
             return View();
         }


    }
}
