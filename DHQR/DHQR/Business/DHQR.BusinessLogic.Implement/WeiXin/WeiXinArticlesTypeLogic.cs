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
    /// 文章类型
    /// </summary>
    public class WeiXinArticlesTypeLogic : BaseLogic<WeiXinArticlesType>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();

        private WeiXinArticlesTypeRep _repository { get { return new WeiXinArticlesTypeRep(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinArticlesType> Repository
        {
            get { return _repository; }
        }
    }
}
