using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{

    /// <summary>
    /// 角色与功能项实体操作类
    /// </summary>
    public class RoleToActionRepository : ProRep<RoleToAction>
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public RoleToActionRep()
        //{
        //    ActiveContext = new BaseDataEntities();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public RoleToActionRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<RoleToAction> EntityCurrentSet
        {
            get { return ActiveContext.RoleToActions; }
        }

        /// <summary>
        /// 获取菜单ID清单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetByUserId(Guid roleId)
        {
            var info = Query(f => f.RoleId == roleId);
            return info.Select(item => item.MenuId);
        }

        /// <summary>
        /// 根据用户ID获取菜单ID清单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetMenuByUserId(Guid userId)
        {
            var roleIds = ActiveContext.UserModuleRoles.Where(f => f.UserId == userId).Select(t => t.ModuleRoleId).ToList();
            var roles = ActiveContext.ModuleRoles.Where(f => roleIds.Contains(f.Id));
            var isSuperAdmin = roles.Any(f => f.Code == "SuperAdmin");
            if (isSuperAdmin)
            {
                return ActiveContext.Menus.Select(f => f.Id).ToList();
            }
            else
            {
                var info = Query(f => roleIds.Contains(f.RoleId));
                return info.Select(item => item.MenuId).Distinct();
            }
        }


        ///// <summary>
        ///// 保存配置
        ///// </summary>
        ///// <param name="actionList"></param>
        ///// <param name="roleId"></param>
        ///// <param name="menuId"></param>
        //public static void ConfigSave(string actionList, Guid roleId, Guid menuId)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void DelByRoleMnu(Guid roleId, Guid menuId)
        //{
        //   var data = rep
        //}
        public void CleanByRoleId(Guid roleId)
        {
            string strcmd = string.Format("delete from RoleToActions WHERE RoleId ='{0}'", roleId);
            var resultCount = ActiveContext.ExecuteStoreCommand(strcmd);
        }
    }

}
