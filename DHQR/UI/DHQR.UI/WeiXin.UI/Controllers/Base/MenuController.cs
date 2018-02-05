using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;
using DHQR.DataAccess.Entities;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            //var service = new MenuModelService();
            //service.GetAll();
            return View();
        }

        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new MenuModelService();
            PagedResults<MenuModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetAll() 
        {
            var service = new MenuModelService();
            var result = service.GetAll().OrderBy(f=>f.Sequence).ToList();
            return Json(result);
        }

        /// <summary>
        /// 获取可见菜单
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetVisibleMenu()
        {
            var service = new MenuModelService();
            var result = service.GetAll().Where(f=>f.IsVisible==true).OrderBy(f => f.Sequence).ToList();
            return Json(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id) 
        {
            var service = new MenuModelService();
            MenuModel model = new MenuModel();
            if (id.HasValue)
            {
                model = service.GetByKey(id.Value);
            }
            ViewData["parentList"] = service.GetParentMenuSelList();
            return View(model);
        }




        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(MenuModel menuModel)
        {
            var service = new MenuModelService();
            DoHandle doHandle;
            service.Add(menuModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(MenuModel menuModel)
        {
            var service = new MenuModelService();
            DoHandle doHandle;
            service.Edit(menuModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(MenuModel menuModel)
        {
            var service = new MenuModelService();
            DoHandle doHandle;
            service.Del(menuModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 获取下拉列表树
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetTreeList()
        {
            var service = new MenuModelService();
            var Trees = service.GetAllTree();
            return Json(Trees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetUserMenuTree()
        {
            var service = new MenuModelService();
            var tree = service.GetTreeByLoginName(GetLogonName());
            return Json(tree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetUserMenuByUser()
        {
            var service = new MenuModelService();
            var tree = service.GetMenuByUser(GetLogonName());
            return Json(tree);
        }
  



        /// <summary>
        /// 获取已经有的树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetRoleMenuTree(Guid roleId)
        {
            var service = new MenuModelService();
            var tree = service.GetTreeByRoleId(roleId);
            return Json(tree);
        }

        /// <summary>
        /// 获取分配的树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetConfigRoleMenuTree(Guid roleId)
        {
            var service = new MenuModelService();
            var tree = service.GetConfigTreeByRoleId(roleId);
            return Json(tree);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetMenuTree() 
        {
            var service = new MenuModelService();
            var userName = GetLogonName();
            var tree = service.GetMenuTree(userName);
            return Json(tree);
        }
    }
}
