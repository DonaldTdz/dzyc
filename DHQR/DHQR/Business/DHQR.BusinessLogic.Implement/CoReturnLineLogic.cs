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
    /// 退货单明细逻辑层
    /// </summary>
    public class CoReturnLineLogic : BaseLogic<CoReturnLine>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CoReturnLineRepository CoReturnLineRep { get { return new CoReturnLineRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CoReturnLine> Repository
        {
            get { return CoReturnLineRep; }
        }



    }
}
