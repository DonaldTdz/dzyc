using System;
using Basic.DAl;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using System.Linq;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信群发历史逻辑层
    /// </summary>
    public class WeiXinMassMsgHistLogic : BaseLogic<WeiXinMassMsgHist>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinMassMsgHistRepository repository { get { return new WeiXinMassMsgHistRepository(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinMassMsgHist> Repository
        {
            get { return repository; }
        }

        public WeiXinMassMsgHist GetByMsgid(string msgid)
        {
            return repository.GetByMsgid(msgid);
        }
    }
}
