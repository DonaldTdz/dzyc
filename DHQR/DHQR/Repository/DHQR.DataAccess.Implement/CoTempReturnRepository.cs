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
    /// 暂存订单数据访问层
    /// </summary>
    public class CoTempReturnRepository : ProRep<CoTempReturn>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public CoTempReturnRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<CoTempReturn> EntityCurrentSet
        {
            get { return ActiveContext.CoTempReturns; }
        }

        /// <summary>
        /// 判断是否是暂存订单
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public bool IsCoTempReturn(string DIST_NUM, string CO_NUM)
        {
            var isCoTemp = ActiveContext.CoTempReturns.Any(f => f.OUT_DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            return isCoTemp;
        }

    }
}
