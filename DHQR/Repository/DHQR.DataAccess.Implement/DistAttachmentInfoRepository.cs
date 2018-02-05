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
    /// 到货确认数据访问层
    /// </summary>
    public class DistAttachmentInfoRepository : ProRep<DistAttachmentInfo>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistAttachmentInfoRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistAttachmentInfo> EntityCurrentSet
        {
            get { return ActiveContext.DistAttachmentInfos; }
        }

    }
}
