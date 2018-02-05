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
    /// 用户对应功能项逻辑层
    /// </summary>
    public class UserFastModuleLogic : BaseLogic<UserFastModule>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private UserFastModuleRepository repository { get { return new UserFastModuleRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<UserFastModule> Repository
        {
            get { return repository; }
        }
    }

}
