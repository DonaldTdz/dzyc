using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送任务单订单商品信息数据访问层
    /// </summary>
    public class LdmDisItemRepository : ProRep<LdmDisItem>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public LdmDisItemRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<LdmDisItem> EntityCurrentSet
        {
            get { return ActiveContext.LdmDisItems; }
        }

    }
}
