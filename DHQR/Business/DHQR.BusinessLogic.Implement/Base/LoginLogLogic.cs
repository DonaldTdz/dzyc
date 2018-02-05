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
    /// 用户登录逻辑层 
    /// </summary>
    public class LoginLogLogic : BaseLogic<LoginLog>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LoginLogRepository LoginLogRep { get { return new LoginLogRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LoginLog> Repository
        {
            get { return LoginLogRep; }
        }



    }
}
