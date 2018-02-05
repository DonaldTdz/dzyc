using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class WeiXinUserTypeController : WeiXinBaseController
    {
        //
        // GET: /WeiXinUserType/
        private readonly WeiXinUserTypeToModuleModelService userTypeToModelService;

        public WeiXinUserTypeController()
        {
            userTypeToModelService = new WeiXinUserTypeToModuleModelService();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户类型对应功能项配置
        /// </summary>
        /// <returns></returns>
        public ActionResult TypeToModule()
        {
            return View();
        }


        public ActionResult EditOrAdd(Guid? Id)
        {
            return null;
        }

        //新增自定义URL
        public JsonResult Create(WeiXinUserTypeModel model)
        {
            DoHandle doHandle;
            model.Id = Guid.NewGuid();
            WeiXinUserTypeModelService modelService = new WeiXinUserTypeModelService();
            modelService.Create(model, out doHandle);
            return Json(doHandle);

        }

        //编辑自定义URL
        public JsonResult Edit(WeiXinUserTypeModel model)
        {
            DoHandle doHandle;
            WeiXinUserTypeModelService modelService = new WeiXinUserTypeModelService();
            modelService.Update(model, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinUserTypeModelService modelService = new WeiXinUserTypeModelService();
            modelService.Delete("WeiXinUserTypes", id, out doHandle);
            return JsonForDoHandle(doHandle);


        }

        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinUserTypeModelService modelService = new WeiXinUserTypeModelService();
            PagedResults<WeiXinUserTypeModel> result = modelService.GetPageData(param);
            return JsonForGrid(result);
        }

        #region 用户类型对应权限

        /// <summary>
        /// 根据用户类型ID获取功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetModuleByTypeId(Guid typeId)
        {
            ModuleModelService modelService = new ModuleModelService();
            var result = modelService.GetModuleByTypeId(typeId);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 获取未选择的功能项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult GetUnSelModuleByTypeId(Guid typeId)
        {
            ModuleModelService modelService = new ModuleModelService();
            var result = modelService.GetUnSelModuleByTypeId(typeId);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <returns></returns>
        public JsonResult AddModuleToType(string moduleIds, Guid typeId)
        {
            DoHandle dohandle;
            userTypeToModelService.AddModuleToType(moduleIds, typeId, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 删除功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult DelModuleToType(string moduleIds, Guid typeId)
        {
            DoHandle dohandle;
            userTypeToModelService.DelModuleToType(moduleIds,typeId,out dohandle);
            return JsonForDoHandle(dohandle);
        }

        
        /// <summary>
        /// 根据类型获取微信用户
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult GetUserByTypeId(Guid typeId)
        {
            WeiXinUserModelService service = new WeiXinUserModelService();
            var result = service.GetUserByTypeId(typeId);
            return JsonForGrid(result);
        }

        #endregion


    }
}
