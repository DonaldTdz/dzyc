using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 物流信息
    /// </summary>
    public class WeiXinLogisticRep : ProRep<WeiXinLogistic>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinLogisticRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinLogistic> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinLogistics; }
        }


        /// <summary>
        /// 根据订单唯一码获取
        /// </summary>
        /// <param name="Unicode"></param>
        /// <returns></returns>
        public WeiXinLogistic GetByUniCode(string Unicode)
        {
            var result = ActiveContext.WeiXinLogistics.SingleOrDefault(f => f.UniCode == Unicode);
            return result;
        }

    }
}
