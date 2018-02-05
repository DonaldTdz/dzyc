using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public class ExceptionLogController : BaseController
    {
        //
        // GET: /ExceptionLog/
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new ExceptionLogModelService();
            PagedResults<ExceptionLogModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="ExceptionLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(ExceptionLogModel ExceptionLogModel)
        {
            var service = new ExceptionLogModelService();
            DoHandle doHandle;
            service.Add(ExceptionLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ExceptionLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(ExceptionLogModel ExceptionLogModel)
        {
            var service = new ExceptionLogModelService();
            DoHandle doHandle;
            service.Edit(ExceptionLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="ExceptionLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(ExceptionLogModel ExceptionLogModel)
        {
            var service = new ExceptionLogModelService();
            DoHandle doHandle;
            service.Del(ExceptionLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


    }
}
