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
    /// 用户对应功能角色逻辑层
    /// </summary>
    public class UserModuleRoleLogic : BaseLogic<UserModuleRole>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private UserModuleRoleRepository repository { get { return new UserModuleRoleRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<UserModuleRole> Repository
        {
            get { return repository; }
        }
    }

}
