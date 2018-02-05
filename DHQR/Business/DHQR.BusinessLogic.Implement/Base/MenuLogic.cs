using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 菜单逻辑层 
    /// </summary>
    public class MenuLogic : BaseLogic<Menu>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private MenuRepository MenuRep { get { return new MenuRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<Menu> Repository
        {
            get { return MenuRep; }
        }

        

        /// <summary>
        /// 根据登录名获取菜单
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetByLoginName(string loginName)
        {
            var userRep = new UserRepository(_baseDataEntities);
            var user = userRep.GetByLoginName(loginName);
            var roleToActionRep = new RoleToActionRepository(_baseDataEntities);
            IEnumerable<Guid> menuId = roleToActionRep.GetMenuByUserId(user.Id);
            return GetByKeys(menuId);
        }


        /// <summary>
        /// 根据功能角色ID获取菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetTreeByRoleId(Guid roleId)
        {
            var roleRep = new ModuleRoleRepository(_baseDataEntities);
            var role = roleRep.GetByKey(roleId);
            if (role.Name == "SuperAdmin")//判断是否超级用户
            {
                return GetAll();
            }
            var roleToActionRep = new RoleToActionRepository(_baseDataEntities);
            IEnumerable<Guid> menuId = roleToActionRep.GetByUserId(roleId);
            return GetByKeys(menuId);
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetMenuByParentId(Guid parentId) 
        {
            var data = _baseDataEntities.Menus.Where(f => f.ParentMenuID == parentId);
            return data;
        }
    }
}
