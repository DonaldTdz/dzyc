using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using Common.BLL.Implement;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;
using System.Data.Common;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 上传附件逻辑层
    /// </summary>
    public class AttachmentInfoLogic : BaseLogic<AttachmentInfo>
    {

        readonly DHQREntities _baseDataEntities = new DHQREntities();

        private AttachmentInfoRepository Rep { get { return new AttachmentInfoRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<AttachmentInfo> Repository
        {
            get { return Rep; }
        }

    }
}
