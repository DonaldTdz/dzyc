using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 异常日志逻辑层
    /// </summary>
    public class ServiceExceptionLogLogic : BaseLogic<ServiceExceptionLog>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ServiceExceptionLogRepository repository { get { return new ServiceExceptionLogRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ServiceExceptionLog> Repository
        {
            get { return repository; }
        }


    }
}
