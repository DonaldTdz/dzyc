using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 异常日志数据访问层
    /// </summary>
    public class ServiceExceptionLogRepository : ProRep<ServiceExceptionLog>
    {

         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ServiceExceptionLogRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ServiceExceptionLog> EntityCurrentSet
        {
            get { return ActiveContext.ServiceExceptionLogs; }
        }


    }
}
