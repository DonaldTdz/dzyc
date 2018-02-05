using System;
using System.Data.Objects;
using DHQR.DataAccess.Entities;
using System.Linq;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 微信群发媒体信息数据访问层
    /// </summary>
    public class WeiXinMediaRepository : ProRep<WeiXinMedia>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinMediaRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinMedia> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinMedias; }
        }

    }
}
