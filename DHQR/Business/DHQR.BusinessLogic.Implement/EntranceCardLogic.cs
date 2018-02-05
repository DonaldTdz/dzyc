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
    /// 出入门卡逻辑层
    /// </summary>
    public class EntranceCardLogic : BaseLogic<EntranceCard>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private EntranceCardRepository repository { get { return new EntranceCardRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<EntranceCard> Repository
        {
            get { return repository; }
        }


    }
}
