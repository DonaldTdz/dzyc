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
    /// 微信群发历史数据访问层
    /// </summary>
    public class WeiXinMassMsgHistRepository : ProRep<WeiXinMassMsgHist>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinMassMsgHistRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinMassMsgHist> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinMassMsgHists; }
        }

        public WeiXinMassMsgHist GetByMsgid(string msgid)
        {
            return ActiveContext.WeiXinMassMsgHists.SingleOrDefault(f => f.msg_id == msgid);
        }
    }
}
