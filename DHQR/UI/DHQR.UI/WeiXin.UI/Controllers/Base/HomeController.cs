using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.DHQRCommon;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Default()
        {
            //var data = new UserModelService().GetByName(GetUserName());
            //if (data.IsPart)
            //{

            //    return RedirectToAction("Index", "Member");
            //} 
            ViewData["UserName"] = GetLogonName();
            return View();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Main()
        {
            //var data = new UserModelService().GetByName(GetUserName());
            //if (data.IsPart)
            //{

            //    return RedirectToAction("Index", "Member");
            //} 
            ViewData["UserName"] = GetLogonName();
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult About()
        {
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Register()
        {
            return View();
        }
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Info()
        {
            ViewData["CurrentUser"] =GetCurrentUser().Nickname;
            int distCount, finishCount, notfinishCount;
            decimal totalMoney;
            LdmDistModelService modelService = new LdmDistModelService();
            modelService.GetLdmDistInfo(out distCount, out finishCount, out notfinishCount, out totalMoney);
            ViewData["DistCount"] = distCount;
            ViewData["FinishCount"] = finishCount;
            ViewData["NotfinishCount"] = notfinishCount;
            ViewData["TotalMoney"] = totalMoney;
            return View();
        }



    }
}
