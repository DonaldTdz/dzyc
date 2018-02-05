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
    /// 全局配置数据访问层
    /// </summary>
    public class GlobalConfigurationRepository : ProRep<GlobalConfiguration>
    {
        #region  基础

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public GlobalConfigurationRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<GlobalConfiguration> EntityCurrentSet
        {
            get { return ActiveContext.GlobalConfigurations; }
        }


        #endregion

        /// <summary>
        /// 根据键取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GlobalConfiguration GetValueByKey(string key)
        {
            var result = ActiveContext.GlobalConfigurations.SingleOrDefault(f => f.Key == key);
            return result;
        }
    }
}
