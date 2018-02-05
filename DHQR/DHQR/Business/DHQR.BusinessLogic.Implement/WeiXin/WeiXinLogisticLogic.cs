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
    /// 物流信息逻辑层
    /// </summary>
    public class WeiXinLogisticLogic : BaseLogic<WeiXinLogistic>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinLogisticRep repository { get { return new WeiXinLogisticRep(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinLogistic> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 根据订单唯一码获取
        /// </summary>
        /// <param name="Unicode"></param>
        /// <returns></returns>
        public WeiXinLogistic GetByUniCode(string Unicode)
        {
            var result = repository.GetByUniCode(Unicode);
            return result;
        }

    }
}
