using System.Data.Objects;
using DHQR.DataAccess.Entities;
using System.Linq;
using System;


namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 用户实体操作类
    /// </summary>
    public class WeiXinFirstInRep : ProRep<WeiXinFirstIn>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinFirstInRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinFirstIn> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinFirstIns; }
        }

        //根据微信Id获取数据
        public  WeiXinFirstIn GetByWeiXinAppId(Guid weiXinAppId)
        {
            WeiXinFirstIn result = EntityCurrentSet.SingleOrDefault(p => p.WeiXinAppId == weiXinAppId);
            return result;
        }
    }
}
