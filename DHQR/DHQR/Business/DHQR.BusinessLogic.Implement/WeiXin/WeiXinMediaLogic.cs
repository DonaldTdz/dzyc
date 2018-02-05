using System;
using Common.Base;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信群发媒体信息逻辑层
    /// </summary>
    public class WeiXinMediaLogic : BaseLogic<WeiXinMedia>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinMediaRepository repository { get { return new WeiXinMediaRepository(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinMedia> Repository
        {
            get { return repository; }
        }

    }
}
