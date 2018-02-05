using Common.BLL.Implement;
using Common.DAL.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 
    /// </summary>
    public class WeiXinCustomUrlLogic : BaseLogic<WeiXinCustomUrl>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinCustomUrlRep _repository { get { return new WeiXinCustomUrlRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinCustomUrl> Repository
        {
            get { return _repository; }
        }
    }
}
