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
    /// 暂存逻辑层
    /// </summary>
    public class CoTempLogic : BaseLogic<CoTemp>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CoTempRepository repository { get { return new CoTempRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CoTemp> Repository
        {
            get { return repository; }
        }
    }
}
