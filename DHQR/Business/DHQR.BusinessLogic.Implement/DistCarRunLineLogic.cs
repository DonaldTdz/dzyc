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
    /// 车辆运行明细逻辑层
    /// </summary>
    public class DistCarRunLineLogic : BaseLogic<DistCarRunLine>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistCarRunLineRepository DistCarRunLineRep { get { return new DistCarRunLineRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistCarRunLine> Repository
        {
            get { return DistCarRunLineRep; }
        }


         /// <summary>
        /// 查询车辆运行信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunLine> QueryCarRunLine(DistCarRunLineQueryParam queryParam)
        {
            return DistCarRunLineRep.QueryCarRunLine(queryParam);
        }
    }
}
