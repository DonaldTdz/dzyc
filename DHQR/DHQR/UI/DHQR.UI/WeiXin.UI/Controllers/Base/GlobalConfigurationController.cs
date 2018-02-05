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
    public class GlobalConfigurationController : BaseController
    {
        //
        // GET: /GlobalConfiguration/

        /// <summary>
        /// 全局参数配置
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            return View();
        }


        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new GlobalConfigurationModelService();
            PagedResults<GlobalConfigurationModel> data = service.GetPageData(queryParam);
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
            var service = new GlobalConfigurationModelService();
            GlobalConfigurationModel model = new GlobalConfigurationModel();
            if (id.HasValue)
            {
                model = service.GetByKey(id.Value);
            }
            ViewData["DataTypes"] = service.GetDataTypeList();
            return View(model);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(GlobalConfigurationModel model)
        {
            var service = new GlobalConfigurationModelService();
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            service.Add(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(GlobalConfigurationModel model)
        {
            var service = new GlobalConfigurationModelService();
            DoHandle doHandle;
            service.Edit(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(GlobalConfigurationModel model)
        {
            var service = new GlobalConfigurationModelService();
            DoHandle doHandle;
            service.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

    }
}
