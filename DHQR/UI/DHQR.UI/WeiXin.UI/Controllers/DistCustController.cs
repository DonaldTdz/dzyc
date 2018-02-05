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
    /// 到货确认控制层
    /// </summary>
    public partial class DistCustController : BaseController
    {
        //
        // GET: /DistCust/
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 时间点查看
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult ViewTimeRecord()
        {

            return View();
        }

        /// <summary>
        /// 查询时间节点
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult QueryTimeRecord(TimeRecordQueryParam queryParam)
        {
            DistCustModelService modelservice = new DistCustModelService();
            var datas = modelservice.QueryTimeRecord(queryParam);
            var result = JsonForGrid(datas);
            return JsonForGrid(datas);
        }

        /// <summary>
        /// 手动出库
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult HandleCarOut(string DLVMAN_ID, string LOG_DATE)
        {
            DistCustModelService modelservice = new DistCustModelService();
            DoHandle dohandle = new DoHandle();
            modelservice.HandleCarOut(DLVMAN_ID, LOG_DATE, out dohandle);
            return JsonForDoHandle(dohandle);

        }

        /// <summary>
        /// 手动入库
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult HandleCarIn(string DLVMAN_ID, string LOG_DATE)
        {
            DistCustModelService modelservice = new DistCustModelService();
            DoHandle dohandle = new DoHandle();
            modelservice.HandleCarIn(DLVMAN_ID, LOG_DATE, out dohandle);
            return JsonForDoHandle(dohandle);
        }



    }

    
    }