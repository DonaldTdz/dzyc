using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public class RoleToActionController : BaseController
    {


        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new RoleToActionModelService();
            PagedResults<RoleToActionModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="RoleToActionModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(RoleToActionModel RoleToActionModel)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            service.Add(RoleToActionModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RoleToActionModel"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(RoleToActionModel RoleToActionModel)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            service.Edit(RoleToActionModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="RoleToActionModel"></param>
        /// <returns></returns>
      [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(RoleToActionModel RoleToActionModel)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            service.Del(RoleToActionModel, out doHandle);
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


        /// <summary>
        /// 保存
        /// </summary> 
        /// <param name="form"></param>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
      [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Save(FormCollection form, Guid roleId, Guid menuId)
        {
            var service = new RoleToActionModelService();
            DoHandle doHandle;
            service.Save(form["actionList[]"], roleId, menuId, out doHandle);
            return JsonForDoHandle(doHandle);
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
    }
}
