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
    /// 暂存订单逻辑层
    /// </summary>
    public class CoTempReturnLogic : BaseLogic<CoTempReturn>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CoTempReturnRepository repository { get { return new CoTempReturnRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CoTempReturn> Repository
        {
            get { return repository; }
        }

    }
}
