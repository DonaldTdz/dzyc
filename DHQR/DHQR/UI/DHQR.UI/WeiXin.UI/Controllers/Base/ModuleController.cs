using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 功能项
    /// </summary>
    public class ModuleController : BaseController
    {

        private readonly ModuleModelService service;

        public ModuleController()
        {
            service = new ModuleModelService();
        }

        /// <summary>
        /// 首页
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
            PagedResults<ModuleModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetAll()
        {
            var result = service.GetAll().ToList();
            return Json(result);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(ModuleModel model)
        {
            DoHandle doHandle;
            service.Add(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(ModuleModel model)
        {
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
        public JsonResult Del(ModuleModel model)
        {
            DoHandle doHandle;
            service.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }
    }
}
