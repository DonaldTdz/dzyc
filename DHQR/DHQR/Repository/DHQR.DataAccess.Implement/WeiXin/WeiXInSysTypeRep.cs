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
    /// 微信系统管理模块类型
    /// </summary>
    public class WeiXInSysTypeRep : ProRep<WeiXinSysType>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXInSysTypeRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinSysType> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinSysTypes; }
        }


        /// <summary>
        /// 根据APPID获取模块类型
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<WeiXinSysType> GetByAppId(Guid appId)
        {
            var result = ActiveContext.WeiXinSysTypes.Where(f => f.WeiXinAppId == appId).ToList() ;
            return result;
        }
    }
}
