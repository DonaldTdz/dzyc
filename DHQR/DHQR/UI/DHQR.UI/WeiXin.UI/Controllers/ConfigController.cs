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
    /// 配置
    /// </summary>
    public class ConfigController : BaseController
    {
        //
        // GET: /Config/
         [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 配送单下载日期
        /// </summary>
        /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreModule)]
         public ActionResult DistDate()
         {
             DistDateConfigModelService modelService=new DistDateConfigModelService();
             DistDateConfigModel model = modelService.GetDistDateConfig();
             return View(model);
         }


         /// <summary>
         /// 配送单下载日期
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreModule)]
         public JsonResult SaveDistDateConfig(DistDateConfigModel model)
         {
             DistDateConfigModelService modelService = new DistDateConfigModelService();
             DoHandle dohandle;
             modelService.SaveDistDateConfig(model,out dohandle);
             return JsonForDoHandle(dohandle);
         }



    }
}
