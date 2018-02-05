using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;

namespace DHQR.UI.Models.Base
{
    /// <summary>
    /// 功能权限对应菜单
    /// </summary>
    #region 模型
    public class RoleToActionModel
    {

        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public string ActionList { get; set; }

    }
    #endregion


    /// <summary>
    /// 功能权限对应菜单
    /// </summary>
    #region Services
    public class RoleToActionModelService : BaseModelService<RoleToAction, RoleToActionModel>
    {
        private readonly RoleToActionLogic _RoleToActionLogic;
        public RoleToActionModelService()
        {
            _RoleToActionLogic = new RoleToActionLogic();
        }

        protected override BaseLogic<RoleToAction> BaseLogic
        {
            get { return _RoleToActionLogic; }
        }


        public PagedResults<RoleToActionModel> GetPageData(QueryParam queryParam)
        {
            return _RoleToActionLogic.Search(queryParam).ConvertTo(ConvertToModel);
        }



        public void Save(string actionList, Guid roleId, Guid menuId, out DoHandle doHandle)
        {
            _RoleToActionLogic.ConfigSave(actionList, roleId, menuId, out doHandle);
        }

        public void DelByMenuRole(RoleToActionModel model, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            var c = _RoleToActionLogic.SingleOrDefualt(f => f.MenuId == model.MenuId && f.RoleId == model.RoleId);

            if (c != null)
            {
                _RoleToActionLogic.Delete(c, out doHandle);
            }
        }

        public void SaveInfo(List<Guid> s, Guid roleId, out DoHandle doHandle)
        {
            _RoleToActionLogic.SaveInfo(s, roleId, out doHandle);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="doHandle"></param>
        internal void CleanRole(Guid roleId, out DoHandle doHandle)
        {
            _RoleToActionLogic.CleanRole(roleId, out doHandle);
        }
    }

    #endregion
}