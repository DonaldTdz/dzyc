using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Base;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 功能角色业务逻辑层
    /// </summary>
    public class ModuleRoleLogic : BaseLogic<ModuleRole>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ModuleRoleRepository repository { get { return new ModuleRoleRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ModuleRole> Repository
        {
            get { return repository; }
        }


        /// <summary>
        /// 向功能角色添加用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddUserToRole(IList<Guid> userIds, Guid roleId, out DoHandle dohandle)
        {
            repository.AddUserToRole(userIds, roleId, out dohandle);
        }

        /// <summary>
        /// 删除功能角色对应用户
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelUserToRole(IList<Guid> userRoleIds, Guid roleId, out DoHandle dohandle)
        {
            repository.DelUserToRole(userRoleIds, roleId, out dohandle);

        }


        /// <summary>
        /// 向功能角色添加功能项
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToRole(IList<Guid> moduleIds, Guid roleId, out DoHandle dohandle)
        {
            repository.AddModuleToRole(moduleIds, roleId, out dohandle);

        }

        /// <summary>
        /// 删除功能角色对应功能项
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToRole(IList<Guid> mtomIds, Guid roleId, out DoHandle dohandle)
        {
            repository.DelModuleToRole(mtomIds, roleId, out dohandle);

        }

        /// <summary>
        /// 根据用户Id获取功能角色清单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ModuleRole> GetRolesByUser(Guid userId)
        {
            return repository.GetRolesByUser(userId);
        }

    }

}
