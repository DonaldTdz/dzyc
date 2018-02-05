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
    /// 功能项逻辑层
    /// </summary>
    public class ModuleLogic : BaseLogic<Module>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ModuleRepository repository { get { return new ModuleRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<Module> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 根据功能角色ID获取功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Module> GetModuleByRoleId(Guid roleId)
        {
            return repository.GetModuleByRoleId(roleId);
        }

        /// <summary>
        /// 根据用户类型ID获取功能项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<Module> GetModuleByTypeId(Guid typeId)
        {
            return repository.GetModuleByTypeId(typeId);
        }

    }

}
