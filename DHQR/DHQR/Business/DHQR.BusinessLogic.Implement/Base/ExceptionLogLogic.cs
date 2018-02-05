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
    /// 异常日志逻辑层 
    /// </summary>
    public class ExceptionLogLogic : BaseLogic<ExceptionLog>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ExceptionLogRepository ExceptionLogRep { get { return new ExceptionLogRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ExceptionLog> Repository
        {
            get { return ExceptionLogRep; }
        }



    }
}
