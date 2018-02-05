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

    /// <summary>
    /// 服务调用日志控制层
    /// </summary>
    public class ServiceCallLogController : BaseController
    {
        //
        // GET: /ServiceCallLog/

        private readonly ServiceCallLogModelService modelService;

        public ServiceCallLogController()
        {
            modelService = new ServiceCallLogModelService();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 查询服务调用日志
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            PagedResults<ServiceCallLogModel> data = modelService.GetPageData(queryParam);
            return JsonForGrid(data);
        }



    }
}
