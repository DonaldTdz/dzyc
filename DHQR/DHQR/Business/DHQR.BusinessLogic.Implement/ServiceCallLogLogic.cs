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
    /// 回程登记流水号逻辑层
    /// </summary>
    public class ServiceCallLogLogic : BaseLogic<ServiceCallLog>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ServiceCallLogRepository repository { get { return new ServiceCallLogRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ServiceCallLog> Repository
        {
            get { return repository; }
        }


         /// <summary>
        /// 新增服务器请求日志
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <param name="MethodName"></param>
        /// <param name="RequestParam"></param>
        /// <param name="UserName"></param>
        public void InsertLog(string ServiceName, string MethodName, string RequestParam, string UserName, bool IsSucessful)
        {
            repository.InsertLog(ServiceName, MethodName, RequestParam, UserName, IsSucessful);   
        }

    }
}
