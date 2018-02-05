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
    /// 服务调用日志数据访问层
    /// </summary>
    public class ServiceCallLogRepository : ProRep<ServiceCallLog>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ServiceCallLogRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ServiceCallLog> EntityCurrentSet
        {
            get { return ActiveContext.ServiceCallLogs; }
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
            ServiceCallLog log = new ServiceCallLog 
            {
                Id=Guid.NewGuid(),
                ServiceName=ServiceName,
                MethodName=MethodName,
                RequestParam=RequestParam,
                RequestTime=DateTime.Now,
                IsSucessful=IsSucessful,
                UserName=UserName
            };
            ActiveContext.ServiceCallLogs.AddObject(log);
            try
            {
                ActiveContext.SaveChanges();
            }
            catch(Exception ex)
            { 

            }
        }

    }
}
