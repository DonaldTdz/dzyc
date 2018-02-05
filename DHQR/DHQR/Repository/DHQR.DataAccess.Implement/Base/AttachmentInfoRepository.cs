using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    //上传附件数据访问层
   public class AttachmentInfoRepository : ProRep<AttachmentInfo>
    {
       public AttachmentInfoRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
       protected internal override ObjectSet<AttachmentInfo> EntityCurrentSet
        {
            get { return ActiveContext.AttachmentInfos; }
        }

    }
}
