using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;
using DHQR.DataAccess.Implement;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeiXinAppRep : ProRep<WeiXinApp>
    {
                /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinAppRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinApp> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinApps; }
        }

        /// <summary>
        /// 根据用户ID获取公众号(1.0版本只返回一个账号)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public WeiXinApp GetOneByUserId(Guid userId)
        {
            var result = ActiveContext.WeiXinApps.FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 根据微信KEY值获取公众号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public WeiXinApp GetByWeiXinKey(string key)
        {
            var result = ActiveContext.WeiXinApps.FirstOrDefault(f => f.WeiXinKey == key);
            return result;
        }

    }
}
