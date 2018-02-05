using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using Common.DAL.Implement;
using Basic.DAl;
using DHQR.DataAccess.Entities;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 用户逻辑层 
    /// </summary>
    public class WeiXinTriggerInfoLogic : BaseLogic<WeiXinTriggerInfo>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinTriggerInfoRep weiXinTriggerInfoRep { get { return new WeiXinTriggerInfoRep(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinTriggerInfo> Repository
        {
            get { return weiXinTriggerInfoRep; }
        }

    }
}
