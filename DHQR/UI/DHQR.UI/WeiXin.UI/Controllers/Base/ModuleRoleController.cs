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
    /// 功能角色
    /// </summary>
    public class ModuleRoleController : BaseController
    {

        private readonly ModuleRoleModelService service;

        public ModuleRoleController()
        {
            service = new ModuleRoleModelService();
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
            PagedResults<ModuleRoleModel> data = service.GetPageData(queryParam);
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
        public JsonResult Add(ModuleRoleModel model)
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
        public JsonResult Edit(ModuleRoleModel model)
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
        public JsonResult Del(ModuleRoleModel model)
        {
            DoHandle doHandle;
            if (model.Code == "SuperAdmin")
            {
                doHandle = new DoHandle { IsSuccessful = false, OperateMsg = "不允许删除超级管理员角色!" };

            }
            else
            {
                service.Del(model, out doHandle);
            }
            return JsonForDoHandle(doHandle);
        }

       /// <summary>
       /// 根据功能角色ID获取功能项
       /// </summary>
       /// <param name="roleId"></param>
       /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetModuleByRoleId(Guid roleId)
        {
            var result = new ModuleModelService().GetModuleByRoleId(roleId);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 根据功能角色ID获取用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetUserByRoleId(Guid roleId)
        {
            var result = new UserModelService().GetUserByRoleId(roleId);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 根据功能角色ID获取可选择功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetUnSelModuleByRoleId(Guid roleId)
        {
            var result = new ModuleModelService().GetUnSelModuleByRoleId(roleId);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 根据功能角色ID获取可选择的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetUnselUserByRoleId(Guid roleId)
        {
            var result = new UserModelService().GetUnselUserByRoleId(roleId);
            return JsonForGrid(result);
        }



        /// <summary>
        /// 向功能角色添加用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult AddUserToRole(string userIds,Guid roleId)
        {
            DoHandle dohandle;
            service.AddUserToRole(userIds, roleId, out dohandle);
            return JsonForDoHandle(dohandle);
        }

        /// <summary>
        /// 向功能角色添加功能项
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult AddModuleToRole(string moduleIds, Guid roleId)
        {
            DoHandle dohandle;
            service.AddModuleToRole(moduleIds, roleId, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 删除功能角色对应用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult DelUserToRole(string userIds, Guid roleId)
        {
            DoHandle dohandle;
            service.DelUserToRole(userIds, roleId, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 删除功能角色对应功能项
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult DelModuleToRole(string moduleIds, Guid roleId)
        {
            DoHandle dohandle;
            service.DelModuleToRole(moduleIds, roleId, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        //----------------功能角色对应菜单----------------------------------------
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult RoleToAction(Guid roleId, string name)
        {
            ViewData["RoleId"] = roleId;
            ViewData["RoleName"] = name;
            return View();
        }

        /// <summary>
        /// 保存
        /// </summary> 
        /// <param name="form"></param>
        /// <param name="roleId"></param> 
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult SaveInfo(FormCollection form, Guid roleId)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            var ids = form["ids[]"];
            if (ids == null)
            {
                service.CleanRole(roleId, out doHandle);
                return JsonForDoHandle(doHandle);
            }
            var c = ids.Split(',');
            var menuIds = c.Select(t => new Guid(t)).ToList();
            service.SaveInfo(menuIds, roleId, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="RoleToActionModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult DelByMenuRole(RoleToActionModel RoleToActionModel)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            service.DelByMenuRole(RoleToActionModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

    }
}
