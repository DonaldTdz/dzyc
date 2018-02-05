using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 到货确认附件业务层
    /// </summary>
    public class DistAttachmentInfoLogic : BaseLogic<DistAttachmentInfo>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistAttachmentInfoRepository repository { get { return new DistAttachmentInfoRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistAttachmentInfo> Repository
        {
            get { return repository; }
        }


    }

}
