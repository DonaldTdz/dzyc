using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{

    /// <summary>
    /// 照片附件信息数据访问层
    /// </summary>
    public class DistFileLineRepository : ProRep<DistFileLine>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistFileLineRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistFileLine> EntityCurrentSet
        {
            get { return ActiveContext.DistFileLines; }
        }

    }
}
