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
    /// 配送点预警逻辑层
    /// </summary>
    public class DeliveryWarningLogic : BaseLogic<DeliveryWarning>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DeliveryWarningRepository repository { get { return new DeliveryWarningRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DeliveryWarning> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 获取配送点预警信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DeliveryWarning> GetWarningDatas(DeliveryWarningQueryParam queryParam)
        {
            var result = repository.GetWarningDatas(queryParam);
            return result;
        }
    }
}
