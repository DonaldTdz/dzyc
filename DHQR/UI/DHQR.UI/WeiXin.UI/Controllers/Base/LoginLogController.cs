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
    public class LoginLogController : BaseController
    {
        //
        // GET: /LoginLog/
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new LoginLogModelService();
            PagedResults<LoginLogModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 获取所有登陆日志
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetAll() 
        {
            var service = new LoginLogModelService();
            var result = service.GetAll();
            return Json(result);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="LoginLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(LoginLogModel LoginLogModel)
        {
            var service = new LoginLogModelService();
            DoHandle doHandle;
            service.Add(LoginLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="LoginLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(LoginLogModel LoginLogModel)
        {
            var service = new LoginLogModelService();
            DoHandle doHandle;
            service.Edit(LoginLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="LoginLogModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(LoginLogModel LoginLogModel)
        {
            var service = new LoginLogModelService();
            DoHandle doHandle;
            service.Del(LoginLogModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


    }
}
