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
    /// 日志流水号业务层
    /// </summary>
    public class LogKeyLogic : BaseLogic<LogKey>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LogKeyRepository repository { get { return new LogKeyRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LogKey> Repository
        {
            get { return repository; }
        }


        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
           return  repository.GetLogkey();
        }
    }
}
