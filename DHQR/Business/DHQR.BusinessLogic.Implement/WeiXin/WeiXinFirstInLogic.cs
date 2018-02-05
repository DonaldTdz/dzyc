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
    /// 用户逻辑层 
    /// </summary>
    public class WeiXinFirstInLogic : BaseLogic<WeiXinFirstIn>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinFirstInRep weiXinFirstInRep { get { return new WeiXinFirstInRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinFirstIn> Repository
        {
            get { return weiXinFirstInRep; }
        }

        public WeiXinFirstIn GetByWeiXinAppId(Guid weiXinAppId)
        {
            return weiXinFirstInRep.GetByWeiXinAppId(weiXinAppId);
        }
    }
}
