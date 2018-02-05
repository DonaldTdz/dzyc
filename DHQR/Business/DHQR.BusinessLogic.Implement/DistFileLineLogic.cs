using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 照片附件信息逻辑层
    /// </summary>
    public class DistFileLineLogic : BaseLogic<DistFileLine>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistFileLineRepository DistFileLineRep { get { return new DistFileLineRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistFileLine> Repository
        {
            get { return DistFileLineRep; }
        }



    }
}
