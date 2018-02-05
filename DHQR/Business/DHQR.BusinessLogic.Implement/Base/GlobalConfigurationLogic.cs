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
    /// 全局参数配置逻辑层
    /// </summary>
    public class GlobalConfigurationLogic : BaseLogic<GlobalConfiguration>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private GlobalConfigurationRepository repository { get { return new GlobalConfigurationRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<GlobalConfiguration> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 根据键取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GlobalConfiguration GetValueByKey(string key)
        {
            return repository.GetValueByKey(key);
        }
    }
}
