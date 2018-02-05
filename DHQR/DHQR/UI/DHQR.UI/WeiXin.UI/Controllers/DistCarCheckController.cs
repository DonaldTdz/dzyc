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
    /// 车辆检查控制层
    /// </summary>
    public class DistCarCheckController : BaseController
    {

        private readonly DistCarCheckModelService modelService;


        public DistCarCheckController()
        {
            modelService = new DistCarCheckModelService();
        }

        /// <summary>
        /// 车辆运行信息
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询车辆检查记录
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetCarCheckResult(DistCarCheckQueryParam queryParam)
        {
            var datas = modelService.GetCarCheckResult(queryParam);
            var result = JsonForGrid(datas);
            return JsonForGrid(datas);
        }


    }
}
