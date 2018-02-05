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
using System.IO;


namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 出入门卡控制层
    /// </summary>
    public class EntranceCardController : BaseController
    {
        //
        // GET: /EntranceCard/
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new EntranceCardModelService();
            PagedResults<EntranceCardModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id)
        {
            var service = new EntranceCardModelService();
            EntranceCardModel model = new EntranceCardModel();
            if (id.HasValue)
            {
                model = service.GetByKey(id.Value);
            }
            ViewData["dptList"] = service.GetDptList();
            return View(model);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(EntranceCardModel model)
        {
            var service = new EntranceCardModelService();
            DoHandle doHandle;
            model.GlobalCode = model.GlobalCode.ToUpper();
            model.CreateTime = DateTime.Now;
            service.Add(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(EntranceCardModel model)
        {
            var service = new EntranceCardModelService();
            DoHandle doHandle;
            model.GlobalCode = model.GlobalCode.ToUpper();
            service.Edit(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(EntranceCardModel model)
        {
            var service = new EntranceCardModelService();
            DoHandle doHandle;
            service.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

    }
}
