using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 退货单明细数据访问层
    /// </summary>
    public class CoReturnLineRepository : ProRep<CoReturnLine>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public CoReturnLineRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<CoReturnLine> EntityCurrentSet
        {
            get { return ActiveContext.CoReturnLines; }
        }
    }
}
