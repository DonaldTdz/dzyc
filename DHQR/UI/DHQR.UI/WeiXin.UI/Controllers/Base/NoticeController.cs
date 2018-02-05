using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class NoticeController : Controller
    {
        //
        // GET: /Notice/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 提交成功
        /// </summary>
        /// <returns></returns>
        public ActionResult Success(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        /// <summary>
        /// 提交失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult Error(string msg)
        {
            return View();
        }
    }
}
