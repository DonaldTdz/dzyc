using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;
using System.Data.Common;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 角色对应功能项
    /// </summary>
    public class RoleToActionLogic : BaseLogic<RoleToAction>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private RoleToActionRepository RoleToActionRep { get { return new RoleToActionRepository(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<RoleToAction> Repository
        {
            get { return RoleToActionRep; }
        }

        public void ConfigSave(string actionList, Guid roleId, Guid menuId, out DoHandle doHandle)
        {
            doHandle = null;
            var data = SingleOrDefualt(f => f.RoleId == roleId && f.MenuId == menuId);
            if (data != null)
            {
                Delete(data, out doHandle);
            }
            if (doHandle == null || doHandle.IsSuccessful)
            {
                RoleToActionRep.Create(new RoleToAction { RoleId = roleId, MenuId = menuId, ActionList = actionList ?? "" }, out doHandle);
            }
        }

        public void SaveInfo(List<Guid> s, Guid roleId, out DoHandle doHandle)
        {
            var data = s.Select(item => new RoleToAction()
            {
                RoleId = roleId,
                MenuId = item,
                ActionList = "All"
            }).ToList();
            using (DbConnection con = _baseDataEntities.Connection)
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    RoleToActionRep.CleanByRoleId(roleId); //清理对象
                    Create(data, out doHandle);
                    tran.Commit();
                }
            }
        }

        public void CleanRole(Guid roleId, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            RoleToActionRep.CleanByRoleId(roleId); //清理对象
        }
    }
}
