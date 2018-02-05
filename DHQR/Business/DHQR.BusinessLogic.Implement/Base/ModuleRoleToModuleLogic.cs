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
    /// 功能角色对应功能项业务逻辑层
    /// </summary>
    public class ModuleRoleToModuleLogic : BaseLogic<ModuleRoleToModule>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ModuleRoleToModuleRepository repository { get { return new ModuleRoleToModuleRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ModuleRoleToModule> Repository
        {
            get { return repository; }
        }

    }

}
