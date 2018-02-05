using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 用户实体操作类
    /// </summary>
    public class ExceptionLogRepository : ProRep<ExceptionLog>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ExceptionLogRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ExceptionLog> EntityCurrentSet
        {
            get { return ActiveContext.ExceptionLogs; }
        }
    }
}
